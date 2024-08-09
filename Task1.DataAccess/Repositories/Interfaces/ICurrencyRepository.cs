using Task1.Domain.Entities;

namespace Task1.DataAccess.Repositories.Interfaces
{
    public interface ICurrencyRepository : IRepositoryBase<Rate>
    {
        Task CreateRangeAsync(IEnumerable<Rate> entities, CancellationToken token = default);
    }
}
