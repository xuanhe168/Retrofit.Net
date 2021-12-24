namespace Retrofit.Net.Core.Attributes.Parameters
{
    public class FromBodyAttribute : BaseAttribute
    {
        public FromBodyAttribute():base("")
        {
        }

        public FromBodyAttribute(string name) : base(name)
        {
        }
    }
}