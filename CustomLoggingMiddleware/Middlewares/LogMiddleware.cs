using System.Diagnostics;
using System.Text;

namespace CustomLoggingMiddleware.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMiddleware> _logger;
        private readonly bool _logBody;

        public LogMiddleware(
            RequestDelegate next, 
            ILogger<LogMiddleware> logger, 
            IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _logBody = configuration.GetValue<bool>("LogBody");
        }

        public async Task Invoke(HttpContext context)
        {
            var startTiming = Stopwatch.StartNew();
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var wholeUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
            var httpMethod = context.Request.Method;

            _logger.LogInformation($"Request Timestamp: {timestamp}");
            _logger.LogInformation($"Request Whole URL: {wholeUrl}");
            _logger.LogInformation($"Request HTTP Method: {httpMethod}");

            if (_logBody && context.Request.ContentLength > 0 && context.Request.Body.CanSeek)
            {
                context.Request.EnableBuffering();
                var requestBody = await ReadRequestBody(context);
                _logger.LogInformation($"Request Body: {requestBody}");
                context.Request.Body.Position = 0;
            }
            
            await _next(context);
            startTiming.Stop();
            var elapsedMilliseconds = startTiming.ElapsedMilliseconds;
            _logger.LogInformation($"Response Processing Time: {elapsedMilliseconds} ms");
        }

        private static async Task<string> ReadRequestBody(HttpContext context)
        {
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            return body;
        }
    }
}
