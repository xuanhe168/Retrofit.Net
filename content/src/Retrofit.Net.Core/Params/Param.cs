namespace Retrofit.Net.Core.Params
{
    public class Param
    {
        public string Name { get; set; }
        public dynamic? Value { get; set; }
        public ParamType Type { get; set; }
        public ParamKind Kind { get; set; }

        public Param(string name = "", dynamic? value = null,
            ParamKind kind = ParamKind.Path,ParamType type = ParamType.Text)
        {
            Name = name;
            Value = value;
            Type = type;
            Kind = kind;
        }
    }
}