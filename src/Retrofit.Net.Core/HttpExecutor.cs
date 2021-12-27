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
                            param = parameters[1];
                            dynamic? argument = param.Value;
                            if(argument is not null)
                            {
                                Type type = argument.GetType();
                                if(type.IsClass)
                                {
                                    IList<KeyValuePair<string,dynamic>> fields = ConvertExtensions.GetProperties1(argument);
                                    if(param.Kind == ParamKind.Form)
                                    {
                                        content = new MultipartFormDataContent();
                                        foreach(var item in fields)
                                        {
                                            MultipartFormDataContent multipartFormDataContent = (MultipartFormDataContent)content;
                                            if(item.Value.GetType() != typeof(FieldFile))
                                            {
                                                multipartFormDataContent.Add(new StringContent(item.Value),item.Key);
                                            }
                                            else
                                            {
                                                FieldFile? file = (item.Value as FieldFile);
                                                string? path = file?.FilePath;
                                                multipartFormDataContent.Add(new ByteArrayContent(File.ReadAllBytes(path ?? "")),
                                                    item.Key,file?.FilePath ?? "");
                                            }
                                        }
                                    }else if (param.Kind == ParamKind.Body)
                                    {
                                        foreach (var item in fields)
                                        {
                                            bodyContent.Add(new KeyValuePair<string, string>(item.Key,item.Value.ToString()));
                                        }
                                    }
                                }
                                else // isn't class
                                {
                                    if (param.Kind == ParamKind.Form)
                                    {
                                        content = new MultipartFormDataContent();
                                        MultipartFormDataContent multipartFormDataContent = (MultipartFormDataContent)content;
                                        if(param.Value?.GetType() != typeof(FieldFile))
                                        {
                                            multipartFormDataContent.Add(new StringContent(param.Value),param.Name);
                                        }
                                        else
                                        {
                                            FieldFile? file = (param.Value as FieldFile);
                                            string? path = file?.FilePath;
                                            multipartFormDataContent.Add(new ByteArrayContent(File.ReadAllBytes(path ?? "")),
                                                param.Name, file?.FileName ?? "");
                                        }
                                    }else if(param.Kind == ParamKind.Body)
                                    {
                                        bodyContent.Add(new KeyValuePair<string, string>(param.Name,param.Value?.ToString()));
                                    }
                                }
                            }
                        }
                    }
                    else // first argument is not [FromPath]
                    {
                        if(parameters.Count > 1)throw new ArgumentOutOfRangeException("If annotation no [FromPath] cannot be more than 1");
                        dynamic? argument = param.Value;
                        if(argument is not null)
                        {
                            Type type = argument.GetType();
                            if(type.IsClass)
                            {
                                IList<KeyValuePair<string,dynamic>> fields = ConvertExtensions.GetProperties1(argument);
                                if(param.Kind == ParamKind.Form)
                                {
                                    content = new MultipartFormDataContent();
                                    foreach(var item in fields)
                                    {
                                        MultipartFormDataContent multipartFormDataContent = (MultipartFormDataContent)content;
                                        if(item.Value.GetType() != typeof(FieldFile))
                                        {
                                            multipartFormDataContent.Add(new StringContent(item.Value),item.Key);
                                        }
                                        else
                                        {
                                            FieldFile? file = (item.Value as FieldFile);
                                            string? path = file?.FilePath;
                                            multipartFormDataContent.Add(new ByteArrayContent(File.ReadAllBytes(path ?? "")),
                                                item.Key,file?.FilePath ?? "");
                                        }
                                    }
                                }else if (param.Kind == ParamKind.Body)
                                {
                                    foreach (var item in fields)
                                    {
                                        bodyContent.Add(new KeyValuePair<string, string>(item.Key,item.Value.ToString()));
                                    }
                                }
                            }
                            else // isn't class
                            {
                                if (param.Kind == ParamKind.Form)
                                {
                                    content = new MultipartFormDataContent();
                                    MultipartFormDataContent multipartFormDataContent = (MultipartFormDataContent)content;
                                    if(param.Value?.GetType() != typeof(FieldFile))
                                    {
                                        multipartFormDataContent.Add(new StringContent(param.Value),param.Name);
                                    }
                                    else
                                    {
                                        FieldFile? file = (param.Value as FieldFile);
                                        string? path = file?.FilePath;
                                        multipartFormDataContent.Add(new ByteArrayContent(File.ReadAllBytes(path ?? "")),
                                            param.Name, file?.FileName ?? "");
                                    }
                                }else if(param.Kind == ParamKind.Body)
                                {
                                    bodyContent.Add(new KeyValuePair<string, string>(param.Name,param.Value?.ToString()));
                                }
                            }
                        }
                    }
                }
                Task<HttpResponseMessage> respTask = _client.PostAsync(_method.Path,content);
                respTask.Wait();
                HttpResponseMessage httpResp = respTask.Result;
                response.StatusCode = Convert.ToInt32(httpResp.StatusCode);
                response.Body = httpResp.Content.ReadAsStringAsync().Result;
            }else if (_method.Method == Method.PUT)
            {
                
            }else if (_method.Method == Method.DELETE)
            {
                
            }
            return response;
        }
    }
}