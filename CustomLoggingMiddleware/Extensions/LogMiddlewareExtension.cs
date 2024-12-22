using CustomLoggingMiddleware.Middlewares;

namespace CustomLoggingMiddleware.Extensions
{
    public static class LogMiddlewareExtension
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
