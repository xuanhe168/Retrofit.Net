// using System.Collections.Generic;
// using RestSharp;
// using Retrofit.Net.Core.Attributes.Methods;
// using Retrofit.Net.Core.Attributes.Parameters;
//
// namespace Retrofit.Net.Tests
// {
//     public interface IPeopleService
//     {
//         [HttpGet("people")]
//         RestResponse<List<TestRestCallsIntegration.Person>> GetPeople();
//
//         [HttpGet("people/{id}")]
//         RestResponse<TestRestCallsIntegration.Person> GetPerson(int id);
//
//         [HttpGet("people/{id}")]
//         RestResponse<TestRestCallsIntegration.Person> GetPerson(int id, [FromQuery("limit")] int limit, [FromQuery("test")] string test);
//
//         [HttpPost("people")]
//         RestResponse<TestRestCallsIntegration.Person> AddPerson([FromBody] TestRestCallsIntegration.Person person);
//     }
// }