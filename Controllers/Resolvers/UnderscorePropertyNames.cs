namespace Controllers.Resolvers
{
    using Newtonsoft.Json.Serialization;
    using System.Text.RegularExpressions;

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
