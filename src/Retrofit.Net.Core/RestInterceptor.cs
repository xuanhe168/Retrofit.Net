using System.Reflection;
using Castle.DynamicProxy;

namespace Retrofit.Net.Core
{
    public class RestInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            // 1. 读取请求地址然后和域名拼接得到请求路径

            // 2. 获取请求类型 GET、POST、PUT、DELETE

            // 3. 获取参数信息包括参数类型的判断

            // 4. 发起请求


            // Build Request
            /*var methodInfo = new RestMethodInfo(invocation.Method); // TODO: Memoize these objects in a hash for performance
            var request = new RequestBuilder(methodInfo, invocation.Arguments).Build();

            // Execute request
            var responseType = invocation.Method.ReturnType;
            var genericTypeArgument = responseType.GenericTypeArguments[0];
            // We have to find the method manually due to limitations of GetMethod()
            *//*var methods = restClient.GetType().GetMethods();
            MethodInfo method = methods.Where(m => m.Name == "Execute").First(m => m.IsGenericMethod);
            MethodInfo generic = method.MakeGenericMethod(genericTypeArgument);
            invocation.ReturnValue =  generic.Invoke(restClient, new object[] { request });*//**/

        }
    }
}