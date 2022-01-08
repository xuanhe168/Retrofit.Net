namespace Retrofit.Net.Core.Converts
{
    public interface IConverter
    {
        object? OnConvert(object from, Type to);
    }
}