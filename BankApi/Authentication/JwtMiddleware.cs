using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Authentication
{
    public class JwtMiddleware
    {
        private readonly Microsoft.AspNetCore.Http.RequestDelegate _next; //Helps in triggering the next item in the Request pipeline.
        private readonly AppSettings _appSettings;
        //Use DAO/DataAccess/DAL objects to authenticate the user.
        //private LoginDAL loginDal;
        public JwtMiddleware(
        Microsoft.AspNetCore.Http.RequestDelegate next,
        Microsoft.Extensions.Options.IOptions<AppSettings> settings)
        {
            _next = next;
            _appSettings = settings.Value;
        }
        public async Task Invoke(
        Microsoft.AspNetCore.Http.HttpContext context
        )
        {
            //check whether the context.Request contains the Authorization Header.
            //If it exists, get the header value, split it on space character and extract the last part of
            // the array. Split() returns an array of strings.
            //?. => NUll Conditional operator
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
            if (token is not null)
            {
                try
                {
                    //Create a SecurityToken Handler
                    System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler tokenHandler = new();
                    var key = System.Text.Encoding.UTF8.GetBytes(_appSettings.AppSecretKey);
                    tokenHandler.ValidateToken(
                    token,
                    new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = System.TimeSpan.Zero
                    }, out Microsoft.IdentityModel.Tokens.SecurityToken validatedToken);
                    var jwtToken = validatedToken as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;
                    var username = jwtToken.Claims.First(c => c.Type == "username").Value;
                    context.Items["username"] = username;



                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            //else nothing to be done by the middleware.
            
            //The context.Items will be null and the AuthorizationAttribute will return UnauthorizedResult.
            await _next(context);
        }



    }
}
