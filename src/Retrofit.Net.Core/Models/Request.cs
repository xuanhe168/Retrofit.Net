namespace Retrofit.Net.Core.Models;

public class Request
{
    public string RequestUrl { get; set; }
    public Method Method { get; set; }
    public List<KeyValuePair<string, string>> Headers { get; set; } = new List<KeyValuePair<string, string>>();

    public Request() { }

    public Request(Builder builder)
    {
        Method = builder.Method;
        Headers = builder.Headers;
        RequestUrl = builder.RequestUrl;
    }

    public Builder NewBuilder() => new Builder(this);

    public class Builder
    {
        public string RequestUrl { get; set; }
        public Method Method { get; set; }
        public List<KeyValuePair<string, string>> Headers { get; set; } = new List<KeyValuePair<string, string>>();

        public Builder(Request request)
        {
            Method = request.Method;
            Headers = request.Headers;
            RequestUrl = request.RequestUrl;
        }

        public Builder AddRequestUrl(string value)
        {
            RequestUrl = value;
            return this;
        }

        public Builder AddMethod(Method method)
        {
            Method = method;
            return this;
        }

        public Builder AddHeader(string name,string value)
        {
            Headers.Add(new KeyValuePair<string, string>(name, value));
            return this;
        }

        public Request Build()
        {
            return new Request(this);
        }
    }
}