using Task1.Business.Services.Dtos;
using Task1.Business.Services.Interfaces;
using Task1.Business.Extensions;
using Task1.DataAccess.Repositories.Interfaces;
using Task1.Domain.Entities;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.Extensions.Logging;

namespace Task1.Business.Services.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _repository;
        private readonly string _bankApiUrl;
        private readonly ILogger<CurrencyService> _logger;

        public CurrencyService(ICurrencyRepository repository, string apiUrl, ILogger<CurrencyService> logger)
        {
            _repository = repository;
            _bankApiUrl = apiUrl;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<Rate>>> GetRatesAsync(GetRatesDto dto)
        {
            var response = new Response<IEnumerable<Rate>>();

            try
            {
                if (ServiceExtensions.IsValid(dto.Date, dto.Cur_ID))
                {
                    var predicate = ServiceExtensions.GetPredicate(dto.Date, dto.Cur_ID);

                    var now = DateTime.UtcNow.Date;

                    // no parameters for predicate, checking rates for today
                    predicate ??= (rate) => rate.Date == DateTime.UtcNow;

                    // check data in local db
                    response.Value = await _repository.GetByFilterAsync(predicate);
                    response.Messages.Add("Success");
                }
                else 
                {
                    response.Messages.Add($"Date should be null or <= current Date, cur_ID should be null or > 0, but send date: {dto.Date} cur_ID: {dto.Cur_ID}");
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                response.Messages.Add($"Server error");
            }

            return response;
        }

        public async Task<Response<bool>> SaveRatesAsync(NewRatesDto dto)
        {
            var response = new Response<bool>();

            try
            {
                if (ServiceExtensions.IsValid(dto.Date))
                {
                    var rates = await LoadRatesfromBankApiAsync(dto.Date);

                    if (rates != null)
                    {
                        await _repository.CreateRangeAsync(rates);
                    }

                    response.Value = true;
                    response.Messages.Add("Success");
                }
                else
                {
                    response.Messages.Add($"Date should be null or <= current Date, but send date: {dto.Date}");
                }
            }
            catch (DbUpdateException ex) 
            {
                var sqlException = ex.GetBaseException() as PostgresException;

                if (sqlException != null && sqlException.SqlState == "23505")
                {
                    // Ignore duplicates
                    response.Value = true;
                    response.Messages.Add("The rates for this date have already been loaded into the database");
                }
                else 
                {
                    _logger.LogError(ex.Message);
                    response.Value = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Value = false;
                response.Messages.Add("Server error");
            }

            return response;
        }

        private async Task<IEnumerable<Rate>?> LoadRatesfromBankApiAsync(DateTime? date)
        {
            var client = new HttpClient();

            var url = ServiceExtensions.GetUrl(_bankApiUrl,
                new
                {
                    ondate = date ?? null,
                    periodicity = 0
                }
            );

            var bankResponse = await client.GetAsync(url);
            var json = await bankResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Rate>>(json);
        }
    }
}
