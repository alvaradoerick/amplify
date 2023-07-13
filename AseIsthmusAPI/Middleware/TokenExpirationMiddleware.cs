//namespace AseIsthmusAPI.Middleware
//{
//    public class TokenExpirationMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public TokenExpirationMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            // Check if the token is expired
//            bool isTokenExpired = true; // Your token expiration check logic here

//            if (isTokenExpired)
//            {
//                context.Response.Headers["Www-Authenticate"] = "Bearer realm=\"Your Realm\", error=\"invalid_token\", error_description=\"The token has expired\"";
//                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//                await context.Response.WriteAsync("Token has expired");
//                return;
//            }

//            // Call the next middleware in the pipeline
//            await _next(context);
//        }
//    }
//}
