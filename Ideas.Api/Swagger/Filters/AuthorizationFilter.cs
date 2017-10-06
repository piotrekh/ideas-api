using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ideas.Api.Swagger.Filters
{
    public class AuthorizationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            bool requiresAuthorization = false;
            var authorizeAttribute = context.ApiDescription.ActionAttributes().OfType<AuthorizeAttribute>().FirstOrDefault();

            if (authorizeAttribute != null)
            {
                requiresAuthorization = true;
            }
            else
            {
                //if controller has Authorize attribute and method doesn't allow anonymous access
                authorizeAttribute = context.ApiDescription.ControllerAttributes().OfType<AuthorizeAttribute>().FirstOrDefault();
                bool methodAllowsAnonymous = context.ApiDescription.ActionAttributes().OfType<AllowAnonymousAttribute>().Any();

                if (authorizeAttribute != null && !methodAllowsAnonymous)
                    requiresAuthorization = true;
            }

            if (!requiresAuthorization)
                return;

            operation.Responses.Add("401", new Response { Description = "Unauthorized" });

            if(!string.IsNullOrEmpty(authorizeAttribute.Policy) || !string.IsNullOrEmpty(authorizeAttribute.Roles))
                operation.Responses.Add("403", new Response { Description = "Access Forbidden" });

            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter()
            {
                Name = "Authorization",
                In = "header",
                Type = "string",
                Required = true
            });
        }
    }
}
