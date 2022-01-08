using Newtonsoft.Json;

namespace Retrofit.Net.Core.Converts
{
    public class DefaultJsonConverter : IConverter
    {
        public object? OnConvert(object value, Type to)
        {
            if(value is null)return value;
            if (to == typeof(Stream))return value;
            if (to?.Namespace?.StartsWith("System") is not true)
            {
                return JsonConvert.DeserializeObject(value.ToString() ?? "",to!);
            }
            return value;
        }
    }
}