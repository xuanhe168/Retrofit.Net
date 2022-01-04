namespace Retrofit.Net.Core.Attributes.Parameters
{
    public class FromQueryAttribute : BaseParamAttribute
    {
        public FromQueryAttribute() : base(null) { }

        public FromQueryAttribute(string? name) : base(name)
        {
        }
    }
}
