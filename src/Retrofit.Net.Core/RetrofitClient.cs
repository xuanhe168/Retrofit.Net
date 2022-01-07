using Retrofit.Net.Core.Interceptors;

namespace Retrofit.Net.Core
{
    public class RetrofitClient
    {
        public IAdvancedInterceptor Interceptors;
        public ISimpleInterceptor? SimpleInterceptor;
        public RetrofitClient(Builder builder)
        {
            Interceptors = builder.Interceptor!;
            SimpleInterceptor = builder.SimpleInterceptor;
            Interceptors = new DefaultInterceptor();
        }
        
        public class Builder
        {
            public IAdvancedInterceptor? Interceptor;
            public ISimpleInterceptor? SimpleInterceptor;

            public Builder AddInterceptor(IAdvancedInterceptor _interceptor)
            {
                Interceptor = _interceptor;
                return this;
            }

            public Builder AddInterceptor(ISimpleInterceptor _interceptor)
            {
                SimpleInterceptor = _interceptor;
                return this;
            }

            public RetrofitClient Build() => new RetrofitClient(this);
        }
    }
}
