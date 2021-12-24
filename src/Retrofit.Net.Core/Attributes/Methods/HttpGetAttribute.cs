
namespace Retrofit.Net.Core.Attributes.Methods
{
    public class HttpGetAttribute : BaseAttribute
    {
        public HttpGetAttribute(string path)
        {
            this.Path = path;
        }
    }
}