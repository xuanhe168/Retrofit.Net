using Retrofit.Net.Core.Interceptors;

namespace Retrofit.Net.Core
{
    public class RetrofitClient
    {
        readonly List<IInterceptor> _interceptors = new List<IInterceptor>();

        /// <summary>
        /// Add a new Interceptor
        /// </summary>
        /// <param name="interceptor">New Interceptor</param>
        public void AddInterceptor(IInterceptor interceptor)
        {
            if(interceptor is null)throw new ArgumentNullException($"Argument cannot be null of {nameof(interceptor)}");
            _interceptors.Add(interceptor);
        }

        public RetrofitClient Build()
        {
            return this;
        }

        public static RetrofitClient Builder()
        {
            return new RetrofitClient();
        }
    }
}
