
namespace Retrofit.Net.Core.Attributes.Methods
{
    public class HttpPostAttribute : BaseMethodAttribute
    {
        public HttpPostAttribute(string path)
        {
            this.Path = path;
        }
    }
}