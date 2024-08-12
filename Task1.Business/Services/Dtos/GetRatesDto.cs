using Task1.Domain.Entities;

namespace Task1.Business.Services.Dtos
{
    public class GetRatesDto
    {
        public int? Cur_ID { get; set; }
        public DateTime? Date { get; set; }
    }
}
