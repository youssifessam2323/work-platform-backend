using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using work_platform_backend.Dtos;

namespace work_platform_backend.validation
{
    public class ValidateModelAttribute :ActionFilterAttribute
    {
        public override void  OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                var errorList = (from item in context.ModelState.Values
                        from error in item.Errors
                        select error.ErrorMessage).ToList();
                
                context.Result = new BadRequestObjectResult( new AuthenticationResponse
                {
                    Message = "One or more validation errors occurred.",
                    Errors = errorList
                });
            }
        }
    }
}