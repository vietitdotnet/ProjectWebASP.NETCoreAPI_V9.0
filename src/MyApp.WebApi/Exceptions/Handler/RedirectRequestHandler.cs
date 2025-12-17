using MyApp.Domain.Exceptions;
using MyApp.WebApi.Exceptions.Models;

namespace MyApp.WebApi.Exceptions.Handler
{
    internal sealed class RedirectRequestHandler
     : BaseExceptionHandler<RedirectRequestException>
    {
        public RedirectRequestHandler(ILogger<RedirectRequestException> logger)
            : base(logger) { }

        protected override ApiProblemDetails CreateProblemDetails(
            HttpContext context, RedirectRequestException exception)
        {
            return new ApiProblemDetails
            {
                Title = "Redirect Request",
                ErrorMessage = exception.Message,
                Status = StatusCodes.Status400BadRequest,
                ErrorCode = exception.ErrorCode,
                UrlRedirect = exception.UrlAction,
                TraceId = context.TraceIdentifier,
                Success = false
            };
        }


    }
}
