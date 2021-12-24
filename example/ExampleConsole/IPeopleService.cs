using Retrofit.Net.Core.Attributes.Methods;
using Retrofit.Net.Core.Attributes.Parameters;
using Retrofit.Net.Core.Models;

namespace ExampleConsole
{
    public interface IPeopleService
    {
        [HttpGet("people")]
        Response<List<Person>> GetPeople();

        [HttpGet("people/{id}")]
        Response<Person> GetPerson(int id);

        [HttpGet("people/{id}")]
        Response<Person> GetPerson(int id, [FromQuery("limit")] int limit, [FromQuery("test")] string test);

        [HttpPost("people")]
        Response<Person> AddPerson([FromBody]Person person);
    }
}
