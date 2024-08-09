using Task1.DataAccess.Repositories.Interfaces;
using Task1.Domain.Entities;

namespace Task1.Business.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _repository;
        private readonly string _bankApiUrl;

        public CurrencyService(ICurrencyRepository repository, string apiUrl)
        {
            _repository = repository;
            _bankApiUrl = apiUrl;
        }

        public Task<Response<Rate>> GetRateAsync(GetRatesDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> LoadRatesAsync(NewRatesDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
