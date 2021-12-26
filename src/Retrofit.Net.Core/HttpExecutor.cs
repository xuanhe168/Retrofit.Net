using Retrofit.Net.Core.Builder;
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
                
                List<KeyValuePair<string, string>> kvpsList = new List<KeyValuePair<string, string>>();
                HttpContent content = new FormUrlEncodedContent(kvpsList);
                if(parameters is not null && parameters!.Any())
                {
                    Param? firstParam = parameters[0];
                    if(firstParam.Kind == ParamKind.Path)
                    {
                        if (url.Contains('{')) url = url![0..url.LastIndexOf("{")];
                        url += $"{parameters[0].Value}";
                    }
                    else
                    {
                        // Body,Form
                        string name = firstParam.Name;
                        dynamic? argument = firstParam.Value;
                        if (argument is not null)
                        {
                            Type type = Type.GetType(argument);
                            if (type.IsClass)
                            {

                            }
                            else
                            {
                                // Form,Body
                            }
                        }
                        if (firstParam.Kind is ParamKind.Form)
                        {

                        }else if(firstParam.Kind is ParamKind.Body)
                        {

                        }
                    }

                    if(parameters.Count > 1)
                    {
                        Param param = parameters[1];
                        string name = param.Name;
                        dynamic? argument = param.Value;
                        if (argument is not null)
                        {
                            Type type = Type.GetType(argument);
                            if(type.IsClass)
                            {

                            }
                            else
                            {
                                // Form,Body
                            }
                        }
                    }
                }
                // Body
                if(_method.Parameters is not null && _method.Parameters.Count > 0)kvpsList.AddRange(_method.Parameters!.Select(x => new KeyValuePair<string, string>(x.Name, x.Value)).ToList());
                Task<HttpResponseMessage> respTask = _client.PostAsync(_method.Path,content);
                respTask.Wait();
            }
            return response;
        }
    }
}