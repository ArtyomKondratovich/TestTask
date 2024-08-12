using Microsoft.AspNetCore.Mvc;
using Task1.Business;
using Task1.Business.Services.Dtos;
using Task1.Business.Services.Interfaces;
using Task1.Domain.Entities;

namespace Task1.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            return await _service.SaveRatesAsync(dto);
        }

        [HttpPost]
        [Route("get")]
        public async Task<Response<IEnumerable<Rate>>> GetRate([FromBody] GetRatesDto dto)
        {
            return await _service.GetRatesAsync(dto);
        }
    }
}
