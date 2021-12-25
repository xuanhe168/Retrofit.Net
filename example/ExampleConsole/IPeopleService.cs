using Retrofit.Net.Core.Attributes.Methods;
using Retrofit.Net.Core.Attributes.Parameters;
using Retrofit.Net.Core.Models;

namespace ExampleConsole
{
    public interface IPeopleService
    {
        [HttpGet("/people")]
        Task<Response<List<Person>>> GetPeople();

        [HttpGet("/people/{id}")]
        //Task<Response<int>> GetPerson(int id); // 异步请求
        Response<Person> GetPerson(int id); // 同步请求
        //Task<Response<Person>> GetPerson(int id);

        [HttpGet("/people/{id}")]
        Task<Response<Person>> GetPerson(int id, [FromQuery("limit")] int limit, [FromQuery("test")] string test);

        [HttpPost("/people")]
        Response<Person> AddPerson([FromBody]Person person);
    }
}
