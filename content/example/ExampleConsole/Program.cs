using ExampleConsole;
using ExampleConsole.Models;
using Newtonsoft.Json;
using Retrofit.Net.Core;
using Retrofit.Net.Core.Converts;
using Retrofit.Net.Core.Models;

var client = new RetrofitClient.Builder()
    .AddInterceptor(new HeaderInterceptor())
    .Build();
var retrofit = new Retrofit.Net.Core.Retrofit.Builder()
    .AddBaseUrl("http://localhost:44394")
    .AddClient(client)
    .AddConverterFactory(GsonConverterFactory.Create())
    .Build();
var service = retrofit.Create<IPersonService>();

Response<TokenModel> authResponse = await service.GetJwtToken(new AuthModel() { Name = "admin", Pass = "admin" });
Console.WriteLine(JsonConvert.SerializeObject(authResponse));