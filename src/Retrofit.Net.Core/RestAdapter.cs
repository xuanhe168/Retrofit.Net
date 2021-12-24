/*using Castle.DynamicProxy;

namespace Retrofit.Net.Core
{
    public class RestAdapter
    {
        private static readonly ProxyGenerator _generator = new ProxyGenerator();

        public string Server
        {
            get; set; }

        public T Create<T>() where T : class
        {
            // 创建代理对象拦截对接口 TInterface 成员的调用
            return _generator.CreateInterfaceProxyWithoutTarget<T>(new RestInterceptor());
        }
    }
}*/