![](./Images/demo.png)

## Table of contents
- [About Retrofit.Net](#About-Retrofit.Net)
- [Support Runtime Version](#Support-Runtime-Version)
- [Installing](#Installing)
- [The Restfull with Retrofit.Net](#The-Restfull-with-Retrofit.Net)
  - [Define your Api](#Define-your-Api)
  - [Set up basic url configuration and more](#Set-up-basic-url-configuration-and-more)
    - [Send Get request](#Send-Get-request)
    - [Send Post request](#Send-Get-request)
    - [Send Put request](#Send-Get-request)
    - [Send Delete request](#Send-Get-request)
    - [Uploading multiple files to server by FormData](#Uploading-multiple-files-to-server-by-FormData)
- [Content-type](#Content-type)
- [Retrofit.Net APIs](#Retrofit.Net-APIs)
- [Request Options](#request-options)
- [Response Schema](#response-schema)
- [Interceptors](#interceptors)
  - [Simple interceptor](#Simple-interceptor)
  - [Advanced interceptor](#Advanced-interceptor)
    - [Resolve and reject the request](#Resolve-and-reject-the-request)
- [Transformer](#Transformer)
- [Set proxy and HttpClient config](#set-proxy-and-httpclient-config)
- [Https certificate verification](#https-certificate-verification)
- [Features and bugs](#features-and-bugs)

# About Retrofit.Net
🔥🔥🔥A powerful Http client for .NET, which supports Interceptors, Global configuration, FormData, Request Cancellation, File downloading, Timeout etc. 

# Support Runtime Version
| Target Framework  | Version |  Yes/No |
| --------          | -----:  | :----:  |
| .NET              | 6.x     |   Yes   |
| .NET              | 5.x     |   No   |
| .NET Core         | 3.x     |   No   |
| .NET Core         | 2.x     |   No   |
| .NET Standard     | 2.1     |   No   |
| .NET Standard     | 2.0     |   No   |
| .NET Standard     | 1.x     |   No   |
| .NET Framework    | All     |   No   |

# Installing
```cmd
  dotnet add package RetrofitNet
```

## The Restfull with Retrofit.Net

### Define your Api
```c#
public interface IPersonService
{
  [HttpPost("/api/Auth/GetJwtToken")]
  Response<TokenModel> GetJwtToken([FromForm] AuthModel auth);

  [HttpGet("/api/Person")]
  Response<IList<Person>> Get();

  [HttpPost("/api/Person")]
  Response<Person> Add([FromBody] Person person);

  [HttpGet("/api/Person/{id}")]
  Response<Person> Get([FromPath] int id);

  [HttpPut("/api/Person/{id}")]
  Response<Person> Update([FromPath] int id, [FromBody] Person person);

  [HttpDelete("/api/Person/{id}")]
  Response<Person> Delete([FromPath] int id);
        
  [HttpGet("https://www.baidu.com/index.html")]
  Response<dynamic> GetBaiduHome();
}
```
### Set up basic url configuration and more
```c#
using Retrofit.Net.Core;
using Retrofit.Net.Core.Models;

var client = new RetrofitClient.Builder()
    .AddInterceptor(new HeaderInterceptor())
    .Build();
var retrofit = new Retrofit.Net.Core.Retrofit.Builder()
    .AddBaseUrl("https://localhost:7177")
    .AddClient(client)
    .AddConverterFactory(GsonConverterFactory.Create())
    .Build();
var service = retrofit.Create<IPersonService>();
Response<TokenModel> authResponse = service.GetJwtToken(new AuthModel() { Account = "admin", Password = "admin" });
```
### Send Get request
```c#
Response<IList<Person>> response = await service.Get();
Console.WriteLine(JsonConvert.SerializeObject(response));
```
### Send Post request
```c#
Response<Person> response = await service.Add(new Person { Id = 1,Name = "老中医",Age = 18});
Console.WriteLine(JsonConvert.SerializeObject(response));
```
### Send Put request
```c#
var response = service.Update(1, new Person() { Name = "Charlie" });
```
### Send Delete request
```c#
var response = service.Delete(1);
```
### Uploading multiple files to server by FormData
```c#

```
…you can find all examples code [here](https://github.com/mingyouzhu/Retrofit.Net/tree/master/example/ExampleConsole).

## Content-type
```js
application/json    -> [FromBody]
multipart/form-data -> [FromForm]
```

## Retrofit.Net APIs

### Creating an instance and set default configs.

You can create instance of Retrofit with an optional `Retrofit.Builder` object:

```c#
var client = new RetrofitClient.Builder()
    .AddInterceptor(new HeaderInterceptor()) // Add Interceptor
    .Build();
var retrofit = new Retrofit.Net.Core.Retrofit.Builder()
    .AddBaseUrl("https://localhost:7177")  // Server address
    .AddClient(client)
    .AddConverterFactory(GsonConverterFactory.Create()) // Message Content Converter
    .Build();
```

## Response Schema

The response for a request contains the following information.

```c#
public class Response<T>
{
   // Http message
   public string? Message { get; internal set; }
   // Response body. may have been transformed, please refer to Retrofit.Builder.AddConverterFactory(...).
   public T? Body { get; internal set; }
   // Http status code.
   public int StatusCode { get; internal set; }
   // Response headers.
   public IEnumerable<KeyValuePair<string, object>>? Headers { get; internal set; }
}
```

When request is succeed, you will receive the response as follows:

```c#
Response<IList<Person>> response = await service.Get();
Console.WriteLine(response.Body);
Console.WriteLine(response.Message);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Headers);
```

# Interceptors

For each http request, We can add one or more interceptors, by which we can intercept requests 、 responses and errors.

```c#
... RetrofitClient.Builder()
    .AddInterceptor(new YourCustomInterceptor())
    .Build();
```

## Simple interceptor:
```c#
public class SimpleInterceptorDemo : ISimpleInterceptor
{
    public void OnRequest(Request request)
    {
        Debug.WriteLine($"REQUEST[{request.Method}] => PATH: {request.Path}");
    }

    public void OnResponse(Response<dynamic> response)
    {
        Debug.WriteLine($"RESPONSE[{response.StatusCode}] => Message: {response.Message}");
    }
}
```

## Advanced interceptor
Advanced interceptors can be implemented by inheriting the IAdvancedInterceptor interface. Then I will tell you through an example of token renewal
```c#
public class HeaderInterceptor : IAdvancedInterceptor
{
    public Response<dynamic> Intercept(IChain chain)
    {
        // Get token from local file system
        string? token = null;
        if(File.Exists("token.txt"))token = File.ReadAllText("token.txt");

        // Add token above
        Request request = chain.Request().NewBuilder()
            .AddHeader("Authorization", $"Bearer {token}")
            .Build();

        Response<dynamic> response = chain.Proceed(request);
        if(response.StatusCode == 401)
        {
            // Get a new token and return
            // The way to get the new token here depends on you,
            // you can ask the backend to write an API to refresh the token
            request = chain.Request().NewBuilder()
                .AddHeader("Authorization", $"Bearer <new token>")
                .Build();
            // relaunch!
            response = chain.Proceed(request);
        }
        return response;
    }
}
```

## Resolve and reject the request
In all interceptors, you can interfere with their execution flow. If you want to resolve the request/response with some custom data，you can call `return new Response<dynamic>();`. 
```c#
public Response<dynamic> Intercept(IChain chain)
{
    return new Response<dynamic>();
}
```

## Using application/x-www-form-urlencoded format

By default, Dio serializes request data(except String type) to `JSON`. To send data in the `application/x-www-form-urlencoded` format instead, you can :

```dart
//Instance level
dio.options.contentType= Headers.formUrlEncodedContentType;
//or works once
dio.post(
  '/info',
  data: {'id': 5},
  options: Options(contentType: Headers.formUrlEncodedContentType),
);
```

## Sending FormData

You can also send FormData with Dio, which will send data in the `multipart/form-data`, and it supports uploading files.

```dart
var formData = FormData.fromMap({
  'name': 'wendux',
  'age': 25,
  'file': await MultipartFile.fromFile('./text.txt',filename: 'upload.txt')
});
response = await dio.post('/info', data: formData);
```

There is a complete example [here](https://github.com/flutterchina/dio/blob/master/example/formdata.dart).

### Multiple files upload

There are two ways to add multiple files to ` FormData`， the only difference is that upload keys are different for array types。

```dart
FormData.fromMap({
  'files': [
    MultipartFile.fromFileSync('./example/upload.txt', filename: 'upload.txt'),
    MultipartFile.fromFileSync('./example/upload.txt', filename: 'upload.txt'),
  ]
});
```

The upload key eventually becomes 'files[]'，This is because many back-end services add a middle bracket to key when they get an array of files. **If you don't want “[]”**，you should create FormData as follows（Don't use `FormData.fromMap`）:

```dart
var formData = FormData();
formData.files.addAll([
  MapEntry('files',
    MultipartFile.fromFileSync('./example/upload.txt',filename: 'upload.txt'),
  ),
  MapEntry('files',
    MultipartFile.fromFileSync('./example/upload.txt',filename: 'upload.txt'),
  ),
]);
```

## Transformer

`Transformer` allows changes to the request/response data before it is sent/received to/from the server. This is only applicable for request methods 'PUT', 'POST', and 'PATCH'. Dio has already implemented a `DefaultTransformer`, and as the default `Transformer`. If you want to customize the transformation of request/response data, you can provide a `Transformer` by your self, and replace the `DefaultTransformer` by setting the `dio.transformer`.



### Using proxy

`DefaultHttpClientAdapter` provide a callback to set proxy to `dart:io:HttpClient`, for example:

```dart
import 'package:dio/dio.dart';
import 'package:dio/adapter.dart';
...
(dio.httpClientAdapter as DefaultHttpClientAdapter).onHttpClientCreate = (client) {
  // config the http client
  client.findProxy = (uri) {
    //proxy all request to localhost:8888
    return 'PROXY localhost:8888';
  };
  // you can also create a new HttpClient to dio
  // return HttpClient();
};
```

There is a complete example [here](https://github.com/flutterchina/dio/blob/master/example/proxy.dart).

### Https certificate verification

There are two ways  to verify the https certificate. Suppose the certificate format is PEM, the code like:

```dart
String PEM='XXXXX'; // certificate content
(dio.httpClientAdapter as DefaultHttpClientAdapter).onHttpClientCreate  = (client) {
  client.badCertificateCallback=(X509Certificate cert, String host, int port){
    if(cert.pem==PEM){ // Verify the certificate
      return true;
    }
    return false;
  };
};
```

Another way is creating a `SecurityContext` when create the `HttpClient`:

```dart
(dio.httpClientAdapter as DefaultHttpClientAdapter).onHttpClientCreate  = (client) {
  SecurityContext sc = SecurityContext();
  //file is the path of certificate
  sc.setTrustedCertificates(file);
  HttpClient httpClient = HttpClient(context: sc);
  return httpClient;
};
```

In this way,  the format of certificate must be PEM or PKCS12.

## Copyright & License

This open source project authorized by https://github.com, and the license is MIT.

## Features and bugs

Please file feature requests and bugs at the [issue tracker][tracker].

[tracker]: https://github.com/mingyouzhu/Retrofit.Net/issues

## Donate

Buy a cup of coffee for me (Scan by wechat)：
![Contact-w100](./Images/me.jpg)
![PAY-w100](./Images/pay.jpg)