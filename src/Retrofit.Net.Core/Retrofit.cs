using Castle.DynamicProxy;

namespace Retrofit.Net.Core
{
    public class Retrofit
    {
        static readonly ProxyGenerator _generator = new ProxyGenerator();
        public string BaseUrl { get; private set; }
        RetrofitClient Client { get; set; }

        public Retrofit SetBaseUrl(string url)
        {
            BaseUrl = url;
            return this;
        }

        public Retrofit SetClient(RetrofitClient client)
        {
            Client = client;
            return this;
        }

        public Retrofit Build()
        {
            return this;
        }

        public T Create<T>()where T : class
        {
            return _generator.CreateInterfaceProxyWithoutTarget<T>(new MethodInterceptor(this));
        }

        public static Retrofit Builder()
        {
            return new Retrofit();
        }
    }
}
