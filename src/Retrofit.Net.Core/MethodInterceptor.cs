using Castle.DynamicProxy;
using Newtonsoft.Json;
using Retrofit.Net.Core.Builder;
using Retrofit.Net.Core.Models;

namespace Retrofit.Net.Core
{
    public class MethodInterceptor : IInterceptor
    {
        readonly Retrofit _retrofit;
        public MethodInterceptor(Retrofit retrofit)
        {
            _retrofit = retrofit;
        }

        public void Intercept(IInvocation invocation)
        {
            MethodBuilder methodBuilder = MethodBuilder.Builder()
                .AddBaseUrl(_retrofit.BaseUrl)
                .AddMethodInfo(invocation.Method)
                .AddArguments(invocation.Arguments)
                .AddReturnType(invocation.Method.ReturnType)
                .Build();
            HttpExecutor executor = new HttpExecutor(methodBuilder);
            invocation.ReturnValue = ConvertReturnValue(
                returnType:invocation.Method.ReturnType,
                response:executor.Execute());
        }

        object ConvertReturnValue(Type returnType, Response<dynamic> response)
        {
            if (returnType != null && typeof(Task).IsAssignableFrom(returnType))
            {
                Type[] returnTypes = returnType.GenericTypeArguments;
                // Task<>里面的泛型类型
                Type? response_type = returnTypes[0];

                // 动态创建泛型实体
                object? body_entity = Activator.CreateInstance(response_type);
                // 获取Task泛型内的泛型
                Type response_generic_type = response_type.GenericTypeArguments[0];
                object? bodyValue = JsonConvert.DeserializeObject(response.Body, response_generic_type);

                // 设置Response的Body属性值
                response_type.GetProperty("Body")!.SetValue(body_entity, bodyValue, null);

                return Task.FromResult(body_entity! as dynamic);
            }
            else
            {
                object? body_entity = Activator.CreateInstance(returnType!);
                Type response_generic_type = returnType!.GenericTypeArguments[0];
                object? bodyValue = JsonConvert.DeserializeObject(response.Body, response_generic_type);

                object? v = Convert.ChangeType(bodyValue, returnType!.GetProperty("Body")!.PropertyType);
                returnType!.GetProperty("Body")!.SetValue(body_entity, v, null);
                return body_entity!;
            }
        }
    }
}