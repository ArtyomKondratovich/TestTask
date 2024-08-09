using System.Linq.Expressions;
using Task1.Domain.Entities;

namespace Task1.Business.Extensions
{
    public static class ServiceExtensions
    {
        public static Expression<Func<Rate, bool>>? GetPredicate(DateTime? date = null, int? cur_ID = null)
        {
            if (cur_ID != null && date != null)
            {
                return (rate) => rate.Cur_ID == cur_ID && rate.Date == date.Value.Date;
            }
            else if (date != null)
            {
                return (rate) => rate.Date == date.Value.Date;
            }
            else if (cur_ID != null) 
            {
                return (rate) => rate.Cur_ID == cur_ID;
            }

            return null;
        }

        public static bool IsValid(DateTime? date = null, int? cur_ID = null) 
        {
            return  (date == null || date.Value.Date <= DateTime.UtcNow.Date)
                && (cur_ID == null || cur_ID > 0);
        }

        public static string GetUrl(string baseUrl, object parameters) 
        {
            var properties = parameters.GetType().GetProperties();

            var queryParams = new List<string>();

            foreach (var property in properties) 
            {
                var value = property.GetValue(parameters);

                if (value != null)
                {
                    var formatValue = value.ToString();

                    if (value is DateTime time)
                    {
                        formatValue = time.ToString("yyyy-MM-dd");
                    }

                    queryParams.Add($"{property.Name}={formatValue}");
                }
            }

            var queryStr = string.Join("&", queryParams);

            return $"{baseUrl}?{queryStr}";
        }
    }
}
