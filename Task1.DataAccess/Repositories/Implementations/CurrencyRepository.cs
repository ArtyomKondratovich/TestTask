using Microsoft.EntityFrameworkCore;
using Task1.DataAccess.Repositories.Interfaces;
using Task1.Domain.Entities;


namespace Task1.DataAccess.Repositories.Implementations
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CurrencyRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }  

        public async Task<Rate?> CreateAsync(Rate entity, CancellationToken token = default)
        {
            var createdRate = await _dbContext.Rates.AddAsync(entity, token);

            if (createdRate != null) 
            {
                await _dbContext.SaveChangesAsync(token);
                return createdRate.Entity;
            }  

            return null;
        }

        public async Task CreateRange(IEnumerable<Rate> entites, CancellationToken token = default)
        {
            await _dbContext.Rates.AddRangeAsync(entites, token);
        }

        public async Task<bool> DeleteAsync(Rate entity, CancellationToken token = default)
        {
            var deletedRate = _dbContext.Remove(entity);

            if (deletedRate != null) 
            {
                await _dbContext.SaveChangesAsync();
            }

            return deletedRate != null;
        }

        public async Task<IEnumerable<Rate>> GetByFilterAsync(System.Linq.Expressions.Expression<Func<Rate, bool>> filter, CancellationToken token = default)
        {
            return await _dbContext.Rates
                .Where(filter)
                .ToListAsync(token);
        }

        public async Task<Rate?> GetByIdAsync(int id, CancellationToken token = default)
        {
            return await _dbContext.Rates
                .FirstOrDefaultAsync(x => x.Cur_ID == id, token);
        }

        public async Task<Rate?> GetByPredicateAsync(System.Linq.Expressions.Expression<Func<Rate, bool>> predicate, CancellationToken token = default)
        {
            return await _dbContext.Rates
                .FirstOrDefaultAsync(predicate, token);
        }

        public async Task<Rate?> UpdateAsync(Rate entity, CancellationToken token = default)
        {
            var updatedRate = _dbContext.Rates.Update(entity);

            if (updatedRate != null)
            {
                await _dbContext.SaveChangesAsync(token);
                return updatedRate.Entity;
            }

            return null;
        }
    }
}
