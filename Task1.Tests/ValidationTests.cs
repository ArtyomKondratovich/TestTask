using Task1.Business.Extensions;

namespace Task1.Tests
{
    public class ValidationTests
    {
        [Theory]
        [InlineData(null, null, true)]
        [InlineData(null, -1, false)]
        [InlineData(null, 12, true)]
        [InlineData(1d, -2, false)]
        [InlineData(1d, 2, false)]
        [InlineData(-1d, -2, false)]
        [InlineData(-1d, 2, true)]
        [InlineData(1d, null, false)]
        [InlineData(-1d, null, true)]
        public void TestQueryParamsForBankApi(double? daysOffset, int? cur_id, bool expected)
        {
            var date = daysOffset == null ? (DateTime?)null : DateTime.UtcNow.AddDays(daysOffset.Value);

            Assert.Equal(ServiceExtensions.IsValid(date, cur_id), expected);
        }
    }
}