using Retrofit.Net.Core.Builder;
using Retrofit.Net.Core.Extensions;
using Retrofit.Net.Core.Models;
using Retrofit.Net.Core.Params;

namespace Retrofit.Net.Core
{
    public class HttpExecutor
    {
        MethodBuilder _method;
        HttpClient _client = new HttpClient();
        public HttpExecutor(MethodBuilder method)
        {
            _method = method;
        }

        public Response<dynamic> Execute()
        {
            Response<dynamic> response = new Response<dynamic>();
            if(_method.Method == Method.GET)
            {
                string url = _method.Path!;
                if(url.Contains("{")) url = url![0..url.LastIndexOf("{")];
                for(int i = 0;i < _method.Parameters!.Count;i++)
                {
                    var param = _method.Parameters[i];
                    if(param.Kind == ParamKind.Query)
                    {
                        if (url.Contains('?') is false)url += "?";
                        url += $"{param.Name}={param.Value}";
                        if (i < (_method.Parameters!.Count - 1)) url += "&";
                    }
                    else if(param.Kind == ParamKind.Path)
                    {
                        url += param.Value;
                    }
                }
                Task<HttpResponseMessage> respTask = _client.GetAsync(url);
                respTask.Wait();
                HttpResponseMessage httpResp = respTask.Result;
                response.StatusCode = Convert.ToInt32(httpResp.StatusCode);
                response.Body = httpResp.Content.ReadAsStringAsync().Result;
            }else if(_method.Method == Method.POST)
            {
                string url = _method.Path!;
                IList<Param>? parameters = _method.Parameters;
                if(parameters?.Count > 2)throw new ArgumentOutOfRangeException("The maximum number of parameters out of range is 2.If there are multiple parameters, please encapsulate them into the class");
                if(parameters?.Count > 1 && parameters!.Any(x => x.Kind == ParamKind.Path)is false)throw new ArgumentException("If there are two parameters, the first parameter must be [FormPath]");
                
                List<KeyValuePair<string, string>> bodyContent = new List<KeyValuePair<string, string>>();
                HttpContent content = new FormUrlEncodedContent(bodyContent);
                if(parameters is not null && parameters!.Any())
                {
                    Param? param = parameters[0];
                    if(param.Kind == ParamKind.Path)
                    {
                        if (url.Contains('{')) url = url![0..url.LastIndexOf("{")];
                        url += $"{parameters[0].Value}";
                        if(parameters.Count > 1)
                        {
                            param = null;
                            param = parameters[1];
                            string name = param.Name;
                            dynamic? argument = param.Value;
                            if(argument is not null)
                            {
                                Type type = argument.GetType();
                                if(type.IsClass)
                                {
                                    IList<KeyValuePair<string,dynamic>> fields = ConvertExtensions.GetProperties1(argument);
                                    if(param.Kind == ParamKind.Form)
                                    {
                                        // TODO Form Processing:
                                    }else if (param.Kind == ParamKind.Body)
                                    {
                                        foreach (var item in fields)
                                        {
                                            bodyContent.Add(new KeyValuePair<string, string>(item.Key,item.Value.ToString()));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if(parameters.Count > 1)throw new ArgumentOutOfRangeException("");
                        string name = param.Name;
                        dynamic? argument = param.Value;
                        if(argument is not null)
                        {
                            Type type = argument.GetType();
                            if(type.IsClass)
                            {
                                IList<KeyValuePair<string,dynamic>> fields = ConvertExtensions.GetProperties1(argument);
                                if(param.Kind == ParamKind.Form)
                                {
                                    // TODO Form Processing:
                                    
                                }else if (param.Kind == ParamKind.Body)
                                {
                                    foreach (var item in fields)
                                    {
                                        bodyContent.Add(new KeyValuePair<string, string>(item.Key,item.Value.ToString()));
                                    }
                                }
                            }
                        }
                    }
                }
                Task<HttpResponseMessage> respTask = _client.PostAsync(_method.Path,content);
                respTask.Wait();
            }
            return response;
        }
    }
}