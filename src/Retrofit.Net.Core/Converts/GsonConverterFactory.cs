namespace Retrofit.Net.Core.Converts
{
    public class GsonConverterFactory : Converter.Factory
    {
        public static GsonConverterFactory Create()
        {
            return new GsonConverterFactory();
            //return create(new Gson());
        }
    }
}
