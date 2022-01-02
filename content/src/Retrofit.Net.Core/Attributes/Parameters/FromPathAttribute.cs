namespace Retrofit.Net.Core.Attributes.Parameters
{
    public class FromPathAttribute : BaseParamAttribute
    {
        public FromPathAttribute() : base(null) { }
        public FromPathAttribute(string? name) : base(name)
        {
        }
    }
}
