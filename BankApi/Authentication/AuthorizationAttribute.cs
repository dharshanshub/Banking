using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BankApi.Authentication
{ 
        //How the attribute will be used. This attribute will be used on class and method declarations.
        [System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Class)]
        public class AuthorizationAttribute: System.Attribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var Username = context.HttpContext?.Items["username"]?.ToString();
                //check whether the username exists. If it does not exist, then return Unauthorized Result
                //to the user.
                if (string.IsNullOrEmpty(Username))
                {
                    context.Result = new JsonResult( new{message = "Unauthorized. Please contact your system administrator for access."})
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }
            }
        }
}

