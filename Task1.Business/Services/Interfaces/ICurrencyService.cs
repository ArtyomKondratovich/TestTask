using Task1.Business.Services.Dtos;
using Task1.Domain.Entities;

namespace Task1.Business.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<Response<bool>> SaveRatesAsync(NewRatesDto dto);
        Task<Response<IEnumerable<Rate>>> GetRatesAsync(GetRatesDto dto);
    }
}
