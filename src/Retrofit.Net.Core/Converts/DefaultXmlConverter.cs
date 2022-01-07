namespace Retrofit.Net.Core.Converts
{
    public class DefaultXmlConverter : IConverter
    {
        public object? OnConvert(string value, Type type)
        {
            // https://stackoverflow.com/questions/10518372/how-to-deserialize-xml-to-object/10518657
            throw new NotImplementedException();
        }
    }
}