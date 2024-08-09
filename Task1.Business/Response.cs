using System.Text.Json.Serialization;

namespace Task1.Business
{
    public class Response<TValue>
    {
        public TValue Value { get; set; }

        public List<string> Messages { get; set; } = new();
    }
}
