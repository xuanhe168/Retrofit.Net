using System.Reflection;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace Retrofit.Net.Core
{
    public class RestInterceptor : IInterceptor
    {
        readonly Core.Retrofit _retrofit;
        public RestInterceptor(Core.Retrofit retrofit)
        {
            _retrofit = retrofit;
        }

        public void Intercept(IInvocation invocation)
        {
            MethodInfo method = invocation.Method;
            var attribute = method.GetCustomAttributes(true)
                .FirstOrDefault(x => x.GetType() == typeof(Attributes.Methods.HttpGetAttribute)) as Attributes.Methods.HttpGetAttribute;
            if (attribute != null)
            {
                string path = attribute.Path;
                string url = $"{_retrofit._BaseUrl}{path}";

                Type returnType = invocation.Method.ReturnType;
                if (returnType != null && typeof(Task).IsAssignableFrom(returnType))
                {
                    Type[] returnTypes = returnType.GenericTypeArguments;
                    if (returnType.FullName == "System.Void") return;
                    Type? response_type = returnTypes[0];

                    // Body
                    object? body_entity = Activator.CreateInstance(response_type);
                    Type response_generic_type = response_type.GenericTypeArguments[0];
                    var json = "{\"id\" : 1,\"name\":\"Onlly\"}";
                    object? bodyValue = JsonConvert.DeserializeObject(json, response_generic_type);

                    // Response
                    object? v = Convert.ChangeType(bodyValue, response_type!.GetProperty("Body")!.PropertyType);
                    response_type.GetProperty("Body")!.SetValue(body_entity, v, null);
                    invocation.ReturnValue = Task.FromResult(body_entity! as dynamic);
                }
                else
                {
                    // Body
                    object? body_entity = Activator.CreateInstance(returnType!);
                    Type response_generic_type = returnType!.GenericTypeArguments[0];
                    var json = "{\"id\" : 1,\"name\":\"Onlly\"}";
                    object? bodyValue = JsonConvert.DeserializeObject(json, response_generic_type);

                    // Response
                    object? v = Convert.ChangeType(bodyValue, returnType!.GetProperty("Body")!.PropertyType);
                    returnType!.GetProperty("Body")!.SetValue(body_entity, v, null);
                    invocation.ReturnValue = body_entity;
                }
            }
        }
    }
}