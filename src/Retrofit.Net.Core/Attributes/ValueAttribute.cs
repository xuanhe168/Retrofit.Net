using System;

namespace Retrofit.Net.Core.Attributes
{
    public class ValueAttribute : Attribute
    {
        public string Value { get; protected set; }
    }
}