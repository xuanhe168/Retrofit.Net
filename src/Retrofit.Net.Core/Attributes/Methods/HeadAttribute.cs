using RestSharp;

namespace Retrofit.Net.Core.Attributes.Methods
{
    [RestMethod(Method.HEAD)]
    public class HeadAttribute : ValueAttribute
    {
        public HeadAttribute(string path)
        {
            this.Value = path;
        }
    }
}