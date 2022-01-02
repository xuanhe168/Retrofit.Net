namespace Retrofit.Net.Core.Attributes.Parameters
{
    public abstract class BaseParamAttribute : Attribute
    {
        public string? Name { get; protected set; }

        protected BaseParamAttribute(string? name)
        {
            Name = name;
        }
    }
}