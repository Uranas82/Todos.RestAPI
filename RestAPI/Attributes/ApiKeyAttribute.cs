using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Persistence.Repositories;


namespace RestAPI.Attributes
{
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var key = context.HttpContext.Request.Headers["ApiKey"].SingleOrDefault();

            if (string.IsNullOrWhiteSpace(key))
            {
                context.Result = new BadRequestObjectResult("ApiKey header is missing");

                return;
            }

            var usersRepository = context.HttpContext.RequestServices.GetService<IUsersRepository>();

            var apiKey = usersRepository.GetApiKey(key);

                if (apiKey is null)
            {
                context.Result = new NotFoundObjectResult("ApiKey is not found");

                return;
            }

                if (!apiKey.IsActive)
            {
                context.Result = new BadRequestObjectResult("ApiKey expired");

                return;
            }

            context.HttpContext.Items.Add("userId", apiKey.UserId);

            await next();
        }
    }
}
