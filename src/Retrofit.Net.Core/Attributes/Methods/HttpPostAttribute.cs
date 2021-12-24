
namespace Retrofit.Net.Core.Attributes.Methods
{
    public class HttpPostAttribute : BaseAttribute
    {
        public HttpPostAttribute(string path)
        {
            this.Path = path;
        }
    }
}