using Castle.DynamicProxy;

namespace Retrofit.Net.Core
{
    public class Retrofit
    {
        public string BaseUrl { get; private set; }
        public RetrofitClient Client { get; private set; }
        public List<Converts.Converter.Factory> ConverterFactories { get; private set; }

        public Retrofit(Builder builder)
        {
            BaseUrl = builder.BaseUrl!;
            Client = builder.Client!;
            ConverterFactories = builder.ConverterFactories;
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
            public readonly List<Converts.Converter.Factory> ConverterFactories =
                new List<Converts.Converter.Factory>();

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

            public Builder AddConverterFactory(Converts.Converter.Factory factory)
            {
                ConverterFactories.Add(factory);
                return this;
            }

            public Retrofit Build()
            {
                return new Retrofit(this);
            }
        }
    }
}
