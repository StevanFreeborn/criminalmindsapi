using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace server.Options.Filters
{
    public class ApiVersionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var headerParameter = operation.Parameters.Where(p => p.Name == "x-api-version").SingleOrDefault();
            
            if (headerParameter != null)
            {
                headerParameter.Description = "Header value that identifies target version of api.";
                headerParameter.Schema.Default = new OpenApiString(context.DocumentName.ToLower().Replace("v", ""));
            }
        }
    }
}
