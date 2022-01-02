namespace Retrofit.Net.Core.Attributes.Parameters
{
    public class FromFormAttribute : BaseParamAttribute
    {
        public FromFormAttribute() : base(null) { }
        public FromFormAttribute(string? name) : base(name)
        {
        }
    }
}
