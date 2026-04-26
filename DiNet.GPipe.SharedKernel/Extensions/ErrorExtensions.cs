using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.SharedKernel.Extensions;

public static class ErrorExtensions
{
    extension(Error error)
    {
        public Result AsResult()
            => Result.Failure(error);

        public Result<T> AsResult<T>()
            => Result.Failure<T>(error);
    }
}
