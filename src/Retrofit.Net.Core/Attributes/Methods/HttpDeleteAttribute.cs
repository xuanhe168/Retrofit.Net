namespace Retrofit.Net.Core.Attributes.Methods
{
    public class HttpDeleteAttribute : BaseAttribute
    {
        public HttpDeleteAttribute(string path)
        {
            this.Path = path;
        }
    }
}