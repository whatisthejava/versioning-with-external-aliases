using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace Aliases
{
    public class ApiVersionOperationFilter : IOperationFilter
    {

        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
            var actionApiVersionModel = context.ApiDescription.ActionDescriptor?.GetApiVersion();
            if (actionApiVersionModel == null)
            {
                return;
            }

            if (actionApiVersionModel.DeclaredApiVersions.Any())
            {
                operation.Produces = operation.Produces
                  .SelectMany(p => actionApiVersionModel.DeclaredApiVersions
                    .Select(version => $"{p};v={version.ToString()}")).ToList();
            }
            else
            {
                operation.Produces = operation.Produces
                  .SelectMany(p => actionApiVersionModel.ImplementedApiVersions.OrderByDescending(v => v)
                    .Select(version => $"{p};v={version.ToString()}")).ToList();
            }
        }
    }
}
