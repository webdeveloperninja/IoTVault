using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Controllers.Resolvers
{
    public class UnderscorePropertyNames : DefaultContractResolver
    {
        public UnderscorePropertyNames() : base()
        {
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            return Regex.Replace(propertyName, @"(\w)([A-Z])", "$1_$2").ToLower();
        }
    }
}
