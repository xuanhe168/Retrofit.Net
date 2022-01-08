namespace Retrofit.Net.Core.Attributes.Methods;

public class HttpGetStream : BaseMethodAttribute
{
    public HttpGetStream(string path)
    {
        this.Path = path;
    }
}