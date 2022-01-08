using Retrofit.Net.Core.Interceptors;

namespace Retrofit.Net.Core
{
    public class RetrofitClient
    {
        public TimeSpan? Timeout;
        public IAdvancedInterceptor Interceptors;
        public ISimpleInterceptor? SimpleInterceptor;
        public RetrofitClient(Builder builder)
        {
            Timeout = builder.Timeout;
            Interceptors = builder.Interceptor ?? new DefaultAdvancedInterceptor();
            SimpleInterceptor = builder.SimpleInterceptor;
        }

        public class Builder
        {
            public TimeSpan? Timeout;
            public IAdvancedInterceptor? Interceptor;
            public ISimpleInterceptor? SimpleInterceptor;

            public Builder AddTimeout(TimeSpan _timeout)
            {
                Timeout = _timeout;
                return this;
            }
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
