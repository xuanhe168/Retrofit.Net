
namespace Retrofit.Net.Core.Attributes.Methods
{
    public class HttpGetAttribute : BaseMethodAttribute
    {
        public HttpGetAttribute(string path)
        {
            this.Path = path;
        }
    }
}