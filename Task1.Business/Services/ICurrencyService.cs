using Task1.Domain.Entities;

namespace Task1.Business.Services
{
    public interface ICurrencyService
    {
        Task<Response<bool>> LoadRatesAsync(NewRatesDto dto);
        Task<Response<Rate>> GetRateAsync(GetRatesDto dto);
    }
}
