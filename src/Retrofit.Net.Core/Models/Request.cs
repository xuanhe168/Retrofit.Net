namespace Retrofit.Net.Core.Models;

public class Request
{
    public Method Method { get; set; }
    public List<KeyValuePair<string, dynamic>> Headers { get; set; } = new List<KeyValuePair<string, dynamic>>();

    public Request() { }

    public Request(Builder builder)
    {
        Method = builder.Method;
        Headers = builder.Headers;
    }

    public Builder NewBuilder() => new Builder(this);

    public class Builder
    {
        public Method Method { get; set; }
        public List<KeyValuePair<string, dynamic>> Headers { get; set; } = new List<KeyValuePair<string, dynamic>>();

        public Builder(Request request)
        {
            Method = request.Method;
            Headers = request.Headers;
        }

        public Builder AddHeader(string name,string value)
        {
            Headers.Add(new KeyValuePair<string, dynamic>(name, value));
            return this;
        }

        public Builder RemoveHeader(string name)
        {
            var item = Headers.Where(x => x.Key == name).FirstOrDefault();
            Headers.Remove(item);
            return this;
        }

        public Request Build()
        {
            return new Request(this);
        }
    }
}