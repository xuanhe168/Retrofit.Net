using Castle.DynamicProxy;
using Retrofit.Net.Core.Converts;

namespace Retrofit.Net.Core
{
    public class Retrofit
    {
        internal string BaseUrl { get; private set; }
        internal RetrofitClient Client { get; private set; }
        internal IConverter? Converter { get; private set;}

        internal Retrofit(Builder builder)
        {
            BaseUrl = builder.BaseUrl!;
            Client = builder.Client!;
            Converter = builder.Converter ?? new DefaultJsonConverter();
        }

        public T Create<T>()where T : class
        {
            ProxyGenerator generator = new ProxyGenerator();
            return generator.CreateInterfaceProxyWithoutTarget<T>(new MethodInterceptor(this));
        }

        public class Builder
        {
            public string? BaseUrl { get; private set; }
            public RetrofitClient? Client { get; private set; }
            public IConverter? Converter { get; private set;}

            public Builder AddBaseUrl(string url)
            {
                BaseUrl = url;
                return this;
            }

            public Builder AddClient(RetrofitClient client)
            {
                Client = client;
                return this;
            }

            public Builder AddConverter(IConverter converter)
            {
                Converter = converter;
                return this;
            }

            public Retrofit Build()
            {
                return new Retrofit(this);
            }
        }
    }
}
