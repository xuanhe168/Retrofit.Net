using Retrofit.Net.Core.Interceptors;

namespace Retrofit.Net.Core
{
    public class RetrofitClient
    {
        public List<IInterceptor> Interceptors;
        public RetrofitClient(Builder builder)
        {
            Interceptors = builder.Interceptors!;
        }
        
        public class Builder
        {
            public List<IInterceptor>? Interceptors { get; private set; }

            public Builder AddInterceptor(IInterceptor interceptor)
            {
                if(Interceptors == null)Interceptors = new List<IInterceptor>();
                if(Interceptors.Any() is false)Interceptors.Add(new DefaultInterceptor());
                Interceptors?.Add(interceptor);
                return this;
            }

            public RetrofitClient Build() => new RetrofitClient(this);
        }
    }
}
