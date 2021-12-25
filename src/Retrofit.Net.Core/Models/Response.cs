namespace Retrofit.Net.Core.Models
{
    public class Response<T>
    {
        public int StatusCode { get; internal set; }
        public T? Body { get; internal set; }
    }
}
