namespace Retrofit.Net.Core.Converts
{
    public interface IConverter
    {
        object? OnConvert(string value, Type type);
    }
}