namespace Retrofit.Net.Core.Attributes.Parameters
{
    public class FromBodyAttribute : BaseParamAttribute
    {
        public FromBodyAttribute():base(null)
        {
        }

        public FromBodyAttribute(string name) : base(name)
        {
        }
    }
}