using System.Diagnostics;
using System.Reflection;
using Retrofit.Net.Core.Attributes.Methods;
using Retrofit.Net.Core.Attributes.Parameters;
using Retrofit.Net.Core.Params;

namespace Retrofit.Net.Core.Builder
{
    public class MethodBuilder
    {
        public MethodInfo MethodInfo { get; set; }
        public object[] Arguments { get; set; }
        public string BaseUrl { get; set; }
        public string? Path { get; set; }
        public Method Method { get; set; }
        public IList<Param>? Parameters { get; set; }
        public Type ReturnType { get;set; }

        public static MethodBuilder Builder() => new MethodBuilder();

        public MethodBuilder AddBaseUrl(string url)
        {
            BaseUrl = url;
            return this;
        }

        public MethodBuilder AddMethodInfo(MethodInfo method)
        {
            MethodInfo = method;
            return this;
        }

        public MethodBuilder AddArguments(object[] arguments)
        {
            Arguments = arguments;
            return this;
        }

        public MethodBuilder AddReturnType(Type returnType)
        {
            ReturnType = returnType;
            return this;
        }

        public MethodBuilder Build()
        {
            BaseUrl = BaseUrl ?? throw new NullReferenceException($"Property cannot be null of {nameof(BaseUrl)}");
            MethodInfo = MethodInfo ?? throw new NullReferenceException($"Property cannot be null of {nameof(MethodInfo)}");
            Arguments = Arguments ?? throw new NullReferenceException($"Property cannot be null of {nameof(Arguments)}");
            ReturnType = ReturnType ?? throw new NullReferenceException($"Property cannot be null of {nameof(ReturnType)}");
            ParseMethodAttributes();
            ParseParameters();
            return this;
        }

        void ParseMethodAttributes()
        {
            BaseMethodAttribute? attribute = MethodInfo.GetCustomAttributes(true)
                .FirstOrDefault() as BaseMethodAttribute;
            attribute = attribute ?? throw new NotImplementedException($"Not annotation found on method {MethodInfo.Name}");
            Path = attribute.Path.Contains("http") != true ? $"{BaseUrl}{attribute!.Path}" : attribute.Path;
            Method = attribute switch
            {
                HttpGetAttribute => Method.GET,
                HttpPostAttribute => Method.POST,
                HttpPutAttribute => Method.PUT,
                HttpDeleteAttribute => Method.DELETE,
                HttpGetStream => Method.STREAM,
                _ => throw new NotSupportedException($"HTTP requests of type '{attribute.GetType().Name}' are not currently supported.")
            };
            Debug.Assert(Path is not null);
        }

        void ParseParameters()
        {
            Parameters = new List<Param>();
            ParameterInfo[] ParamInfos = MethodInfo.GetParameters();
            for(int i = 0;i < ParamInfos.Length;i++)
            {
                ParameterInfo param = ParamInfos[i];
                object value = Arguments[i];
                BaseParamAttribute? attribute = param.GetCustomAttributes(false).FirstOrDefault() as BaseParamAttribute;
                attribute = attribute ?? throw new ArgumentException($"No annotation found on parameter ${param.Name} of ${MethodInfo.Name}");
                Param pm = attribute switch
                {
                    FromPathAttribute => new Param(kind: ParamKind.Path, name: attribute.Name ?? param.Name ?? "", type: ParamType.Text, value: value),
                    FromQueryAttribute => new Param(kind: ParamKind.Query, name: attribute.Name ?? param.Name ?? "", type: ParamType.Text, value: value),
                    FromFormAttribute => new Param(kind: ParamKind.Form, name: attribute.Name ?? param.Name ?? "", type: ParamType.Text, value: value),
                    FromBodyAttribute => new Param(kind: ParamKind.Body, name: attribute.Name ?? param.Name ?? "", type: ParamType.Text, value: value),
                    _ => throw new NotSupportedException($"The annotation of {attribute.GetType().Name} is not supported at the moment")
                };
                Parameters.Add(pm);
            }
        }
    }
}