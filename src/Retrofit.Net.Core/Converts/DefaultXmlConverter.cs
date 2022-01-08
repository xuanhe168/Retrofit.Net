using System.Xml.Serialization;

namespace Retrofit.Net.Core.Converts
{
    public class DefaultXmlConverter : IConverter
    {
        public object? OnConvert(object value, Type to)
        {
            if(value is null)return value;
            if(to == typeof(System.IO.Stream))return value;
            if(to?.Namespace?.StartsWith("System") is not true)
            {
                XmlSerializer serializer = new XmlSerializer(to!);
                TextReader reader = new StringReader(value.ToString() ?? "");
                var obj = serializer.Deserialize(reader);
                reader.Close();
                reader.Dispose();
                return obj;
            }
            return value;
        }
    }
}