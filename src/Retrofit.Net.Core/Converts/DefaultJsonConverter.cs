using Newtonsoft.Json;

namespace Retrofit.Net.Core.Converts
{
    public class DefaultJsonConverter : IConverter
    {
        public object? OnConvert(string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type);
        }
    }
}