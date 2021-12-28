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
        MethodBuilder _method;
        RetrofitClient _retrofitClient;
        Request _request = new Request();
        public HttpExecutor(MethodBuilder method,RetrofitClient client)
        {
            _method = method;
            _retrofitClient = client;
        }

        public Response<dynamic> Execute()
        {
            var interceptor = _retrofitClient.Interceptors.FirstOrDefault();
            return interceptor!.Intercept(this);
        }

        public Response<dynamic> Proceed(Request request)
        {
            Response<dynamic> response = new Response<dynamic>();
            HttpClient _client = new HttpClient();
            Task<HttpResponseMessage>? _responseTask = null;
            string _requestUrl = _method.Path!;
            if (_method.Method == Method.GET)
            {
                _requestUrl = GetParams(_requestUrl, _method.Parameters);
                _responseTask = _client.GetAsync(_requestUrl);
            }
            else if (_method.Method == Method.POST)
            {
                HttpContent? content = GetParams(_method.Parameters);
                _responseTask = _client.PostAsync(_requestUrl, content);
            }
            else if (_method.Method == Method.PUT)
            {
                _requestUrl = GetParams(_requestUrl, _method.Parameters);
                HttpContent? content = GetParams(_method.Parameters);
                _responseTask = _client.PutAsync(_requestUrl, content);
            }
            else if (_method.Method == Method.DELETE)
            {
                _requestUrl = GetParams(_requestUrl, _method.Parameters);
                _responseTask = _client.DeleteAsync(_requestUrl);
            }
            _responseTask?.Wait();
            HttpResponseMessage httpResp = _responseTask!.Result;
            string json = JsonConvert.SerializeObject(httpResp.Headers);
            response.Message = httpResp.ReasonPhrase;
            response.StatusCode = Convert.ToInt32(httpResp.StatusCode);
            response.Body = httpResp.Content.ReadAsStringAsync().Result;
            response.Headers = JsonConvert.DeserializeObject<IEnumerable<KeyValuePair<string, object>>>(json);
            return response;
        }

        public Request Request() => _request;

        string GetParams(string baseUrl,IList<Param>? _params)
        {
            if (baseUrl.Contains("{"))baseUrl = baseUrl[0..baseUrl.LastIndexOf("{")];
            for (int i = 0; i < _params?.Count; i++)
            {
                var param = _params[i];
                if(param.Kind == ParamKind.Query)
                {
                    if(baseUrl.Contains('?') is false)baseUrl += "?";
                    baseUrl += $"{param.Name}={param.Value}";
                    if(i < (_params!.Count - 1))baseUrl += "&";
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
            if(valueType.IsClass)fields = ConvertExtensions.GetProperties(first.Value);
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
                        if(item.Value?.GetType() != typeof(FieldFile))
                        {
                            content.Add(new StringContent(item.Value),item.Key);
                        }
                        else
                        {
                            FieldFile? file = (item.Value as FieldFile);
                            string? path = file?.FilePath;
                            content.Add(new ByteArrayContent(File.ReadAllBytes(path ?? "")),item.Key, file?.FileName ?? "");
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