using ExampleConsole;
using Newtonsoft.Json;
using Retrofit.Net.Core;
using Retrofit.Net.Core.Converts;
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

// 异步请求
/*Console.WriteLine("登录:");
Response<TokenModel> authResponse = await service.GetJwtToken(new AuthModel() { Account = "admin",Password = "admin" });
Console.WriteLine(JsonConvert.SerializeObject(authResponse));

Console.WriteLine("\n\n测试GET请求:");
Response<IList<Person>> response = await service.Get();
Console.WriteLine(JsonConvert.SerializeObject(response));

Console.WriteLine("\n\n测试GET请求带参数:");
Response<Person> response1 = await service.Get(id: 1);
Console.WriteLine(JsonConvert.SerializeObject(response1));

Console.WriteLine("\n\n测试POST请求:");
Response<Person> response2 = await service.Add(new Person { Id = 1,Name = "老中医",Age = 18});
Console.WriteLine(JsonConvert.SerializeObject(response2));

Console.WriteLine("\n\n测试PUT请求:");
Response<Person> response3 = await service.Update(1,new Person { Id = 1, Name = "老中医", Age = 23 });
Console.WriteLine(JsonConvert.SerializeObject(response3));

Console.WriteLine("\n\n测试DELETE请求:");
Response<Person> response4 = await service.Delete(1);
Console.WriteLine(JsonConvert.SerializeObject(response4));*/

// 同步请求
/*Console.WriteLine("登录:");
Response<TokenModel> authResponse = service.GetJwtToken(new AuthModel() { Account = "admin", Password = "admin" });
Console.WriteLine(JsonConvert.SerializeObject(authResponse));

Console.WriteLine("\n\n测试GET请求:");
Response<IList<Person>> response = service.Get();
Console.WriteLine(JsonConvert.SerializeObject(response));

Console.WriteLine("\n\n测试GET请求带参数:");
Response<Person> response1 = service.Get(id: 1);
Console.WriteLine(JsonConvert.SerializeObject(response1));

Console.WriteLine("\n\n测试POST请求:");
Response<Person> response2 = service.Add(new Person { Id = 1, Name = "老中医", Age = 18 });
Console.WriteLine(JsonConvert.SerializeObject(response2));

Console.WriteLine("\n\n测试PUT请求:");
Response<Person> response3 = service.Update(1, new Person { Id = 1, Name = "老中医", Age = 23 });
Console.WriteLine(JsonConvert.SerializeObject(response3));

Console.WriteLine("\n\n测试DELETE请求:");
Response<Person> response4 = service.Delete(1);
Console.WriteLine(JsonConvert.SerializeObject(response4));*/

Console.WriteLine("拉取百度首页:");
Response<dynamic> response5 = service.GetBaiduHome();
Console.WriteLine(JsonConvert.SerializeObject(response5));