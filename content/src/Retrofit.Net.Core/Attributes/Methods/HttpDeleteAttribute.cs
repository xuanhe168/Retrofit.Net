namespace Retrofit.Net.Core.Attributes.Methods
{
    public class HttpDeleteAttribute : BaseMethodAttribute
    {
        public HttpDeleteAttribute(string path)
        {
            this.Path = path;
        }
    }
}