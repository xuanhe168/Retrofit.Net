using Retrofit.Net.Core.Interceptors;

namespace Retrofit.Net.Core
{
    public class RetrofitClient
    {
        public List<IInterceptor> Interceptors = new List<IInterceptor>();
        public RetrofitClient(Builder builder)
        {
            Interceptors = builder.Interceptors!;
            Interceptors.Add(new DefaultInterceptor());
        }
        
        public class Builder
        {
            public List<IInterceptor>? Interceptors = new List<IInterceptor>();

            public Builder AddInterceptor(IInterceptor interceptor)
            {
                Interceptors?.Clear();
                Interceptors?.Add(interceptor);
                return this;
            }

            public RetrofitClient Build() => new RetrofitClient(this);
        }
    }
}
