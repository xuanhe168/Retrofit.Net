using ExampleConsole;
using ExampleConsole.Models;
using Newtonsoft.Json;
using Retrofit.Net.Core;
using Retrofit.Net.Core.Extensions;
using Retrofit.Net.Core.Models;
using static System.Console;

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
Console.WriteLine(person);*/



var builder = RetrofitClient.Builder();
builder.Authenticator();
builder.AddInterceptor();
var client = builder.Build();
var retrofit = Retrofit.Net.Core.Retrofit.Builder()
    .SetBaseUrl("https://localhost:7177")
    .SetClient(client)
    .Build();
var service = retrofit.Create<IPersonService>();

/*WriteLine("测试GET请求:");
Response<IList<Person>> response = await service.Get();
WriteLine();
foreach(var item in response.Body!)
{
    WriteLine(JsonConvert.SerializeObject(item));
}

WriteLine("\n\n测试GET请求带参数:");
Response<Person> response1 = await service.Get(id:1);
WriteLine(JsonConvert.SerializeObject(response1));*/

WriteLine("\n\n测试POST请求:");
Response<Person> response2 = await service.Add(new Person { Id = 1,Name = "老中医",Age = 18});
WriteLine(JsonConvert.SerializeObject(response2));