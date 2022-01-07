using ExampleConsole;
using Newtonsoft.Json;
using Retrofit.Net.Core;
using Retrofit.Net.Core.Converts;

var client = new RetrofitClient.Builder()
    .AddInterceptor(new HeaderInterceptor())
    .AddInterceptor(new SimpleInterceptorDemo())
    .Build();
var retrofit = new Retrofit.Net.Core.Retrofit.Builder()
    .AddBaseUrl("http://localhost:44394")
    .AddClient(client)
    .AddConverter(new DefaultJsonConverter())
    .Build();
var service = retrofit.Create<IPersonService>();

var response = service.GetBaiduHome();
Console.WriteLine(JsonConvert.SerializeObject(response));

/*Response<TokenModel> authResponse = await service.GetJwtToken(new AuthModel() { Name = "admin", Pass = "admin" });
Console.WriteLine(JsonConvert.SerializeObject(authResponse));
File.WriteAllText("token.txt",authResponse.Body?.Token);

Response<ResponsePage<GameEntity>> response = await service.GetGames(new RequestPage(pageNo:1,pageSize:10));
Console.WriteLine(JsonConvert.SerializeObject(response));*/