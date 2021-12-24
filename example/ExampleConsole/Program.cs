using ExampleConsole;
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
Console.WriteLine(person);*/



var builder = RetrofitClient.Builder();
builder.Authenticator();
builder.AddInterceptor();
var client = builder.Build();
var retrofit = Retrofit.Net.Core.Retrofit.Builder()
    .BaseUrl("http://jordanthoms.apiary.io/")
    .Client(client)
    .Build();
var service = retrofit.Create<IPeopleService>();
var response = service.GetPerson(1);
Console.WriteLine(response);