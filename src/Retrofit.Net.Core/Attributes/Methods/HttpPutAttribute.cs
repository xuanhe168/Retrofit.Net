
namespace Retrofit.Net.Core.Attributes.Methods
{
    public class HttpPutAttribute : BaseAttribute
    {
        public HttpPutAttribute(string path)
        {
            this.Path = path;
        }
    }
}