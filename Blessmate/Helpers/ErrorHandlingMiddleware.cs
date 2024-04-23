using System.Net;

namespace Blessmate.Helpers
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
           try{
                await _next(context);
           }
           catch(Exception ex){
                _logger.LogError("Start Global Error Handler");
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                _logger.LogError(context.GetEndpoint().DisplayName);
                _logger.LogError("End Global Error Handler");
                
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
           }
        }
    }
}