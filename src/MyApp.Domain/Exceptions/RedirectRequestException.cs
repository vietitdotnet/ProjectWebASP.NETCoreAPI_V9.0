using MyApp.Domain.Exceptions.APIRoutes;
using MyApp.Domain.Exceptions.CodeErrors;

namespace MyApp.Domain.Exceptions
{
    public class RedirectRequestException : BaseException
    {
        public string UrlAction { get; }

        public RedirectRequestException(
           string message = "Yêu cầu chuyển hướng",
           string urlAction = RedirectRequest.Index,
           string errorCode = RedirectCodes.Default) :
           base(message, errorCode)
        {
            UrlAction = urlAction;
        }
    }
}
