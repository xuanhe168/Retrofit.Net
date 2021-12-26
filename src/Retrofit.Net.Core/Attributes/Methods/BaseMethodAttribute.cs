namespace Retrofit.Net.Core.Attributes.Methods
{
    public abstract class BaseMethodAttribute : Attribute
    {
        public string Path { get;protected set; }
    }
}