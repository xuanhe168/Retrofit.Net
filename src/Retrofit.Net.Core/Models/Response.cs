namespace Retrofit.Net.Core.Models
{
    public class Response<T>
    {
        public string? Message { get; internal set; }
        public T? Body { get; internal set; }
        public int StatusCode { get; internal set; }
        public IEnumerable<KeyValuePair<string, object>>? Headers { get; internal set; }
    }
}
