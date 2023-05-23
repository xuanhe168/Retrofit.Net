using System.Xml.Serialization;
using ExampleConsole;
using ExampleConsole.Models;
using Newtonsoft.Json;
using Retrofit.Net.Core;
using Retrofit.Net.Core.Converts;
using Retrofit.Net.Core.Models;
using ShellProgressBar;

var client = new RetrofitClient.Builder()
    .AddInterceptor(new HeaderInterceptor())
    .AddInterceptor(new SimpleInterceptorDemo())
    .AddTimeout(TimeSpan.FromSeconds(10)) // The default wait time after making an http request is 6 seconds
    .Build();
var retrofit = new Retrofit.Net.Core.Retrofit.Builder()
    .AddBaseUrl("https://localhost:7283") // Base Url
    .AddClient(client)
    .AddConverter(new DefaultXmlConverter()) // The internal default is ‘DefaultJsonConverter’ if you don’t call ‘.AddConverter(new DefaultJsonConverter())’
    .Build();
var service = retrofit.Create<IPersonService>();

/*Response<Stream> response = await service.Download("test");
Stream outStream = File.Create("/Users/onllyarchibald/Desktop/a.zip");

var options = new ProgressBarOptions
{
    ForegroundColor = ConsoleColor.Yellow,
    ForegroundColorDone = ConsoleColor.DarkGreen,
    BackgroundColor = ConsoleColor.DarkGray,
    BackgroundCharacter = '\u2593'
};
var pbar = new ProgressBar((int)response.Body!.Length, "File Downloading", options);

byte[] buffer = new byte[1024];
int i;
do
{
    i = response.Body!.Read(buffer, 0, buffer.Length);
    if (i > 0)
    {
        outStream.Write(buffer, 0, i);
        pbar.Tick((int)outStream.Length,"Downloading...");
    }
} while (i > 0);
outStream.Close();
response.Body.Close();
Console.WriteLine("\nFile download completed...");*/

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


// fix #3
var result = await service.FindPets(121,"BlackWhite");
Console.WriteLine(result.Body);
Console.ReadKey();