﻿using RestSharp;

namespace Retrofit.Net.Core.Attributes.Methods
{
    [RestMethod(Method.GET)]
    public class GetAttribute : ValueAttribute
    {
        public GetAttribute(string path)
        {
            this.Value = path;
        }
    }
}