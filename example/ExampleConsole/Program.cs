using System.Xml.Serialization;
using ExampleConsole;
using ExampleConsole.Models;
using Newtonsoft.Json;
using Retrofit.Net.Core;
using Retrofit.Net.Core.Converts;
using Retrofit.Net.Core.Models;

var client = new RetrofitClient.Builder()
    .AddInterceptor(new HeaderInterceptor())
    .AddInterceptor(new SimpleInterceptorDemo())
    .Build();
var retrofit = new Retrofit.Net.Core.Retrofit.Builder()
    .AddBaseUrl("https://localhost:7283")
    .AddClient(client)
    .AddConverter(new DefaultXmlConverter()) // The internal default is ‘DefaultJsonConverter’ if you don’t call ‘.AddConverter(new DefaultJsonConverter())’
    .Build();
var service = retrofit.Create<IPersonService>();



/*Response<Stream> response = await service.Download("test");
Stream outStream = File.Create("/Users/onllyarchibald/Desktop/a.zip");
byte[] buffer = new byte[1024];
int i;
do{
    i = response.Body!.Read(buffer,0,buffer.Length);
    if(i > 0)outStream.Write(buffer,0,i);
}while(i > 0);
outStream.Close();
response.Body.Close();
Console.WriteLine("File upload completed...");*/

// HttpClient client1 = new HttpClient();
// Stream inStream = await client1.GetStreamAsync("https://localhost:7283/WeatherForecast");
// Stream outStream = File.Create("/Users/onllyarchibald/Desktop/a.zip");
// byte[] buffer = new byte[1024];
// int i;
// do{
//     i = inStream.Read(buffer,0,buffer.Length);
//     if(i > 0)outStream.Write(buffer,0,i);
// }while(i > 0);
// outStream.Close();
// inStream.Close();
// Console.WriteLine($"i = {i}");

/*var response1 = service.Submit(new SubmitEntity{ 
        Name = "老中医",
        File = new FieldFile{ FilePath = "/Users/onllyarchibald/Library/Mobile Documents/iCloud~com~apple~iBooks/Documents/大卫X方法 - 大卫X.pdf" }
    });
Console.WriteLine(JsonConvert.SerializeObject(response1));

/*var response = service.GetWeather();
Console.WriteLine(JsonConvert.SerializeObject(response));*/

//var response = service.GetBaiduHome();
//Console.WriteLine(JsonConvert.SerializeObject(response));

/*Response<TokenModel> authResponse = await service.GetJwtToken(new AuthModel() { Name = "admin", Pass = "admin" });
Console.WriteLine(JsonConvert.SerializeObject(authResponse));
File.WriteAllText("token.txt",authResponse.Body?.Token);

Response<ResponsePage<GameEntity>> response = await service.GetGames(new RequestPage(pageNo:1,pageSize:10));
Console.WriteLine(JsonConvert.SerializeObject(response));*/