using System.Xml.Serialization;

namespace Retrofit.Net.Core.Converts
{
    public class DefaultXmlConverter : IConverter
    {
        public object? OnConvert(string value, Type type)
        {
            // https://stackoverflow.com/questions/10518372/how-to-deserialize-xml-to-object/10518657
            XmlSerializer serializer = new XmlSerializer(type);
            TextReader reader = new StringReader(value);
            var obj = serializer.Deserialize(reader);
            reader.Close();
            reader.Dispose(); 
            return obj;
        }
    }
}