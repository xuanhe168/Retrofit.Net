using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Retrofit.Net.Core.Builder;
using Retrofit.Net.Core.Extensions;
using Retrofit.Net.Core.Interceptors;
using Retrofit.Net.Core.Models;
using Retrofit.Net.Core.Params;
using System.Net.Http.Headers;

namespace Retrofit.Net.Core
{
    public class HttpExecutor : IChain
    {
        Request _request;
        MethodBuilder _method;
        RetrofitClient _retrofitClient;
        public HttpExecutor(MethodBuilder method,RetrofitClient client)
        {
            _method = method;
            _retrofitClient = client;
        }

        public Response<dynamic> Execute()
        {
            _request = new Request().NewBuilder()
                .AddMethod(_method.Method)
                .AddRequestUrl(_method.Path!)
                .Build();
            var interceptor = _retrofitClient.Interceptors;
            return interceptor.Intercept(this);
        }

        public Response<dynamic> Proceed(Request request)
        {
            HttpClient client = new HttpClient();
            client.Timeout = _retrofitClient.Timeout ?? TimeSpan.FromSeconds(6);

            HttpRequestMessage? requestMessage = null;
            if (request.Method == Method.GET)
            {
                var requestUrl = GetUrlByParam(_request.Path,_method.Parameters);
                requestMessage = new HttpRequestMessage(HttpMethod.Get,requestUrl);
            }
            else if (request.Method == Method.POST)
            {
                requestMessage = new HttpRequestMessage(HttpMethod.Post, request.Path);
                HttpContent? content = GetParams(_method.Parameters);
                requestMessage.Content = content;
            }else if (request.Method == Method.PUT)
            {
                var requestUrl = GetUrlByParam(request.Path, _method.Parameters);
                HttpContent? content = GetParams(_method.Parameters);
                requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUrl);
                requestMessage.Content = content;
            }else if (request.Method == Method.DELETE)
            {
                var requestUrl = GetUrlByParam(request.Path, _method.Parameters);
                HttpContent? content = GetParams(_method.Parameters);
                requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUrl);
                requestMessage.Content = content;
            }else if(request.Method == Method.STREAM)
            {
                var requestUrl = GetUrlByParam(_request.Path,_method.Parameters);
                requestMessage = new HttpRequestMessage(HttpMethod.Get,requestUrl);
            }
            foreach (var item in request.Headers)
            {
                if(item.Value is not null)requestMessage?.Headers.Add(item.Key, item.Value);
            }
            _retrofitClient.SimpleInterceptor?.OnRequest(_request);
            HttpResponseMessage responseMessage = client.Send(requestMessage!);
            Response<dynamic> response = new Response<dynamic>();
            string json = JsonConvert.SerializeObject(responseMessage.Headers);
            response.Message = responseMessage.ReasonPhrase;
            response.StatusCode = Convert.ToInt32(responseMessage.StatusCode);
            if(request.Method == Method.STREAM)response.Body = responseMessage.Content.ReadAsStream();
            else response.Body = responseMessage.Content.ReadAsStringAsync().Result;
            response.Headers = JsonConvert.DeserializeObject<IEnumerable<KeyValuePair<string, object>>>(json);
            _retrofitClient.SimpleInterceptor?.OnResponse(response);
            return response;
        }

        public Request Request() => _request;

        string GetUrlByParam(string baseUrl,IList<Param>? _params)
        {
            if (baseUrl.Contains("{"))baseUrl = baseUrl[0..baseUrl.LastIndexOf("{")];
            for (int i = 0; i < _params?.Count; i++)
            {
                Param param = _params[i];
                if (param.Kind == ParamKind.Query)
                {
                    if(baseUrl.Contains('?') is false)baseUrl += "?";

                    Type? valueType = param.Value?.GetType();
                    var name = valueType?.Namespace;
                    if(name?.StartsWith("System") == false)
                    {
                        IList<KeyValuePair<string, dynamic>>? fields = ConvertExtensions.GetProperties(param.Value);
                        foreach(var item in fields)
                        {
                            baseUrl += $"{item.Key}={item.Value}";
                            if(fields.IndexOf(item) < fields.Count - 1)baseUrl += "&";
                        }
                    }
                    else
                    {
                        if (i == 0 && baseUrl.Contains("&")) baseUrl += "&";

                        baseUrl += $"{param.Name}={param.Value}";
                        if (i < (_params!.Count - 1)) baseUrl += "&";
                    }
                }
                else if(param.Kind == ParamKind.Path)baseUrl += param.Value;
            }
            return baseUrl;
        }

        HttpContent? GetParams(IList<Param>? _params)
        {
            HttpContent? response = null;
            if (_params is null || (_params?.Any() ?? false) is false)return null;
            IList<Param> collection = _params.Where(param => param.Kind != ParamKind.Path && param.Kind != ParamKind.Query).ToList();
            if(collection.Count < 1)return null;
            Param first = collection.First();
            Type valueType = first.GetType();
            IList<KeyValuePair<string, dynamic>>? fields = null;
            if(valueType?.Namespace?.StartsWith("System") is not true)fields = ConvertExtensions.GetProperties(first.Value);
            if(first.Kind == ParamKind.Body)
            {
                JObject obj = new JObject();
                if(fields is not null)
                {
                    foreach (var item in fields)
                    {
                        obj.Add(item.Key, item.Value);
                    }
                }
                else
                {
                    obj.Add(first.Name, first.Value);
                }
                if(collection.Count > 1)
                {
                    foreach(var item in collection.Skip(1))
                    {
                        obj.Add(item.Name, item.Value);
                    }
                }
                response = new StringContent(obj.ToString());
                response.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            else if(first.Kind == ParamKind.Form)
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                if(fields is not null)
                {
                    foreach (var item in fields)
                    {
                        
                        object obj = item.Value;
                        Type type = obj.GetType();
                        if(type != typeof(FieldFile))
                        {
                            content.Add(new StringContent(item.Value),item.Key);
                        }
                        else
                        {
                            FieldFile? file = (item.Value as FieldFile);
                            string? path = file?.FilePath;
                            string? filename = Path.GetFileName(path);
                            if(System.IO.File.Exists(path ?? "") is not true)throw new FileNotFoundException($"the file '{path}' must can not be null!");
                            content.Add(new ByteArrayContent(File.ReadAllBytes(path ?? "")),item.Key, filename ?? "");
                        }
                    }
                }
                else
                {
                    content.Add(new StringContent(first.Name,first.Value));
                }
                if(collection.Count > 1)
                {
                    foreach(var item in collection.Skip(1))
                    {
                        content.Add(new StringContent(item.Name, item.Value));
                    }
                }
                response = content;
            }
            return response;
        }
    }
}