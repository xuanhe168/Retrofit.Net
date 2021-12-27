using ExampleConsole.Models;
using Retrofit.Net.Core.Attributes.Methods;
using Retrofit.Net.Core.Attributes.Parameters;
using Retrofit.Net.Core.Models;

namespace ExampleConsole
{
    public interface IPersonService
    {
        [HttpPost("/api/Auth/GetJwtToken")]
        Task<Response<TokenModel>> GetJwtToken([FromForm] AuthModel auth);

        [HttpGet("/api/Person")]
        Task<Response<IList<Person>>> Get();

        [HttpPost("/api/Person")]
        Task<Response<Person>> Add([FromBody] Person person);

        [HttpGet("/api/Person/{id}")]
        Task<Response<Person>> Get([FromPath]int id);

        [HttpPut("/api/Person/{id}")]
        Task<Response<Person>> Update([FromPath] int id, [FromBody] Person person);

        [HttpDelete("/api/Person/{id}")]
        Task<Response<Person>> Delete([FromPath] int id);
    }
}