using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;

namespace PresentationLayer
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            JwtDto jwtDto = jwtUtils.ValidateToken(token);
            if (jwtDto != null)
            {
                context.Items["User"] = userService.Get(jwtDto.UserID);
                //var role = userService.IsCreator(jwtDto.UserID)? "admin": "user";
                //if(role == jwtDto.Role)
                //{
                //    context.Items["Role"] = jwtDto.Role;
                //}
                context.Items["Role"] = jwtDto.Role;
            }

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}

