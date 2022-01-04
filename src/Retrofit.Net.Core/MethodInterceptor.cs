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
            HttpExecutor executor = new HttpExecutor(methodBuilder,_retrofit.Client);
            invocation.ReturnValue = ConvertReturnValue(
                returnType:invocation.Method.ReturnType,
                response:executor.Execute());
        }

        object ConvertReturnValue(Type returnType, Response<dynamic> response)
        {
            if (returnType != null && typeof(Task).IsAssignableFrom(returnType))
            {
                Type[] returnTypes = returnType.GenericTypeArguments;
                Type? response_type = returnTypes[0];
                object? body_entity = Activator.CreateInstance(response_type);
                Type response_generic_type = response_type.GenericTypeArguments[0];
                object? bodyValue;
                try
                {
                    bodyValue = JsonConvert.DeserializeObject(response.Body, response_generic_type);
                }
                catch (Exception ex) { bodyValue = response.Body; }
                response_type.GetProperty("Body")!.SetValue(body_entity, bodyValue, null);
                response_type.GetProperty("Message")!.SetValue(body_entity, response.Message, null);
                response_type.GetProperty("StatusCode")!.SetValue(body_entity, response.StatusCode, null);
                response_type.GetProperty("Headers")!.SetValue(body_entity, response.Headers, null);

                return Task.FromResult(body_entity! as dynamic);
            }
            else
            {
                object? body_entity = Activator.CreateInstance(returnType!);
                Type response_generic_type = returnType!.GenericTypeArguments[0];
                object? bodyValue;
                try
                {
                    bodyValue = JsonConvert.DeserializeObject(response.Body, response_generic_type);
                }catch (Exception ex) { bodyValue = response.Body; }

                returnType.GetProperty("Body")!.SetValue(body_entity, bodyValue, null);
                returnType.GetProperty("Message")!.SetValue(body_entity, response.Message, null);
                returnType.GetProperty("StatusCode")!.SetValue(body_entity, response.StatusCode, null);
                returnType.GetProperty("Headers")!.SetValue(body_entity, response.Headers, null);
                return body_entity!;
            }
        }
    }
}