namespace Retrofit.Net.Core.Attributes.Parameters
{
    public abstract class BaseAttribute : Attribute
    {
        public string Name { get; protected set; }

        protected BaseAttribute(string name)
        {
            Name = name;
        }
    }
}