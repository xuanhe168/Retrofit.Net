using ExampleConsole;
using ExampleConsole.Models;
using Newtonsoft.Json;
using Retrofit.Net.Core;
using Retrofit.Net.Core.Models;

// var builder = RetrofitClient.Builder();
// builder.authenticator(authenticator);
// builder.addInterceptor(tokenInterceptor);

// builder.addNetworkInterceptor(HttpLoggingInterceptor(HttpLoggingInterceptor.Logger { message ->
// Log.i("HTTP", message.toString())
// }).setLevel(HttpLoggingInterceptor.Level.BODY));
// builder.connectTimeout(60, TimeUnit.SECONDS); //60s delay
// builder.readTimeout(60, TimeUnit.SECONDS);
// builder.writeTimeout(60, TimeUnit.SECONDS);
// var client = builder.build();
/*var retrofit = Retrofit.Builder()
                .baseUrl(host_api)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .addCallAdapterFactory(RxJava2CallAdapterFactory.create())
                .client(client)
                .build();*/
// var service = retrofit.Create<IGithubService>();
// var x = service.GetPerson(1);


/*RestAdapter adapter = new RestAdapter("http://jordanthoms.apiary.io/");
IPeopleService service = adapter.Create<IPeopleService>();
Response<Person> personResponse = service.GetPerson(3);
Person person = personResponse.Body;
Console.Console.WriteLine(person);*/



var builder = RetrofitClient.Builder();
builder.Authenticator();
builder.AddInterceptor();
var client = builder.Build();
var retrofit = Retrofit.Net.Core.Retrofit.Builder()
    .SetBaseUrl("https://localhost:7177")
    .SetClient(client)
    .Build();
var service = retrofit.Create<IPersonService>();

Console.WriteLine("登录:");
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
Console.WriteLine(JsonConvert.SerializeObject(response4));