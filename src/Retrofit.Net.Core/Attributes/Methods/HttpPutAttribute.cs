
namespace Retrofit.Net.Core.Attributes.Methods
{
    public class HttpPutAttribute : BaseMethodAttribute
    {
        public HttpPutAttribute(string path)
        {
            this.Path = path;
        }
    }
}