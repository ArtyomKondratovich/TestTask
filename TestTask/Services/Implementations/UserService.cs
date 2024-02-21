using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using TestTask.Enums;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<User> GetUser()
        {
            return await _context.Users
                .GroupJoin(
                _context.Orders,
                u => u.Id,
                o => o.UserId,
                (u, orders) => new
                {
                    u.Id,
                    u.Email,
                    u.Status,
                    OrdersCount = orders.Count()
                })
                .OrderByDescending(x => x.OrdersCount)
                .Select(x => new User()
                {
                    Id = x.Id,
                    Email = x.Email,
                    Status = x.Status
                })
                .FirstAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users
                .Where(x => x.Status == UserStatus.Inactive)
                .ToListAsync();
        }
    }
}
