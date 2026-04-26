using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.WebApi.Infrastructure;


public static class CustomResults
{
    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            title: GetTitle(result.Error),
            detail: GetDetail(result.Error),
            type: GetType(result.Error.ErrorType),
            statusCode: GetStatusCode(result.Error.ErrorType));

        static string GetTitle(Error error) =>
            error.ErrorType switch
            {
                ErrorType.Problem => error.ErrorName,
                ErrorType.NotFound => error.ErrorName,
                ErrorType.Conflict => error.ErrorName,
                _ => "Server failure"
            };

        static string? GetDetail(Error error) =>
            error.ErrorType switch
            {
                ErrorType.Problem => error.ErrorDescription,
                ErrorType.NotFound => error.ErrorDescription,
                ErrorType.Conflict => error.ErrorDescription,
                _ => "An unexpected error occurred"
            };

        static string GetType(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

        static int GetStatusCode(ErrorType errorType) =>
            errorType switch
            {
               ErrorType.Problem => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}
