using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using System;
using System.Linq;

namespace Aliases.Extensions
{
    public class RouteApiReader : IApiVersionReader
    {
        public void AddParameters(IApiVersionParameterDescriptionContext context)
        {
            // Nothing to do 
        }

        public string Read(HttpRequest request)
        {
            var elementsOfPath = request.Path.Value.Split("/").ToList<string>();
            var elementIWant = elementsOfPath.Find(x => x.Length == 2 && x.StartsWith("v"));
            if (elementIWant == null)
            {
                return null;
            }
            return elementIWant.Substring(1);
        }
    }
}
