using ExampleConsole.Models;
using Retrofit.Net.Core.Attributes.Methods;
using Retrofit.Net.Core.Attributes.Parameters;
using Retrofit.Net.Core.Models;

namespace ExampleConsole;

public interface IPersonService
{
    [HttpPost("/api/v1/Auth/GetJwtToken")]
    Task<Response<TokenModel>> GetJwtToken([FromForm] AuthModel auth);

    [HttpGet("/api/Person")]
    Task<Response<IList<Person>>> Get();

    [HttpPost("/api/Person")]
    Task<Response<Person>> Add([FromBody] Person person);

    [HttpGet("/api/Person/{id}")]
    Task<Response<Person>> Get([FromPath] int id);

    [HttpPut("/api/Person/{id}")]
    Task<Response<Person>> Update([FromPath] int id, [FromBody] Person person);

    [HttpDelete("/api/Person/{id}")]
    Task<Response<Person>> Delete([FromPath] int id);

    [HttpGet("/api/v1/Game/Get")]
    Task<Response<ResponsePage<GameEntity>>> GetGames([FromQuery] RequestPage req);

    /*[HttpPost("/api/Auth/GetJwtToken")]
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
    Response<Person> Delete([FromPath] int id);*/

    [HttpGet("https://www.baidu.com/index.html")]
    Response<dynamic> GetBaiduHome();

    [HttpGet("/WeatherForecast/GetWeatherForecast")]
    Response<WeatherEntity> GetWeather();

    [HttpPost("/WeatherForecast/Submit")]
    Response<string> Submit([FromForm] SubmitEntity submit);

    [HttpGetStream("/WeatherForecast/Download")]
    Task<Response<Stream>> Download([FromQuery] string arg1);

    [HttpGet("https://localhost:7225/api/pets/{ownerid}/{color}")]
    Task<Response<string>> FindPets([FromPath] int ownerid, [FromPath] string color);
}