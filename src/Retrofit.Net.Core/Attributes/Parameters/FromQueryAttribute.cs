namespace Retrofit.Net.Core.Attributes.Parameters
{
    public class FromQueryAttribute : BaseAttribute
    {
        public FromQueryAttribute() : base("") { }

        public FromQueryAttribute(string name) : base(name)
        {
        }
    }
}
