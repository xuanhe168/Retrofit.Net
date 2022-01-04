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
            if (BaseUrl is null) throw new NullReferenceException($"Property cannot be null of {nameof(BaseUrl)}");
            if (MethodInfo is null) throw new NullReferenceException($"Property cannot be null of {nameof(MethodInfo)}");
            if (Arguments is null) throw new NullReferenceException($"Property cannot be null of {nameof(Arguments)}");
            if (ReturnType is null) throw new NullReferenceException($"Property cannot be null of {nameof(ReturnType)}");
            ParseMethodAttributes();
            ParseParameters();
            return this;
        }

        void ParseMethodAttributes()
        {
            BaseMethodAttribute? attribute = MethodInfo.GetCustomAttributes(true)
                .FirstOrDefault() as BaseMethodAttribute;
            if (attribute is not null)
            {
                Path = attribute.Path.Contains("http") != true ? $"{BaseUrl}{attribute!.Path}" : attribute.Path;
                if(attribute is HttpGetAttribute)Method = Method.GET;
                else if(attribute is HttpPostAttribute)Method = Method.POST;
                else if(attribute is HttpPutAttribute)Method = Method.PUT;
                else if(attribute is HttpDeleteAttribute)Method = Method.DELETE;
            }
            else throw new NotImplementedException($"Not annotation found on method {MethodInfo.Name}");
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
                if (attribute is null) throw new ArgumentException($"No annotation found on parameter ${param.Name} of ${MethodInfo.Name}");
                if (attribute is FromPathAttribute) Parameters!.Add(new Param(kind:ParamKind.Path,name:attribute.Name ?? param.Name ?? "",type:ParamType.Text,value:value));
                if (attribute is FromQueryAttribute) Parameters!.Add(new Param(kind:ParamKind.Query,name:attribute.Name ?? param.Name ?? "", type: ParamType.Text, value: value));
                if (attribute is FromFormAttribute) Parameters!.Add(new Param(kind:ParamKind.Form,name: attribute.Name ?? param.Name ?? "", type: ParamType.Text, value: value));
                if (attribute is FromBodyAttribute) Parameters!.Add(new Param(kind: ParamKind.Body, name: attribute.Name ?? param.Name ?? "", type: ParamType.Text, value: value));
            }
        }
    }
}