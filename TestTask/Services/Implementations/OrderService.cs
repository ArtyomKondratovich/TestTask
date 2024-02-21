using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Order> GetOrder()
        {
            return await _context.Orders
                .OrderByDescending(x => x.Quantity * x.Price)
                .FirstAsync();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders
                .Where(x => x.Quantity > 10)
                .ToListAsync();
        }
    }
}
