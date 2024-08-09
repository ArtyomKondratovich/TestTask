using Microsoft.AspNetCore.Mvc;
using Task1.Business;
using Task1.Business.Services;
using Task1.Domain.Entities;

namespace Task1.Api.Controllers
{
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _service;

        public CurrencyController(ICurrencyService service) 
        {
            _service = service;
        }

        [HttpPost]
        [Route("load")]
        public async Task<Response<bool>> LoadRates([FromBody] NewRatesDto dto) 
        {
            return await _service.LoadRatesAsync(dto);
        }

        [HttpPost]
        [Route("get")]
        public async Task<Response<Rate>> GetRate([FromBody] GetRatesDto dto)
        {
            return await _service.GetRateAsync(dto);
        }
    }
}
