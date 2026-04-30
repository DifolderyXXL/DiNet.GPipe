namespace DiNet.GPipe.SharedKernel.Results;

public static class ResultExtensions
{
    extension<TIn>(Result<TIn> result)
    {
        public TOut MatchG<TOut>(
            Func<TIn, TOut> onSuccess,
            Func<Result<TIn>, TOut> onFailure)
        {
            return result.IsSuccess ? onSuccess(result.Value!) : onFailure(result);
        }
    }
    extension<TIn>(Result<TIn> result)
    {
        public TOut Match<TOut>(
            Func<TIn, TOut> onSuccess,
            Func<Result<TIn>, TOut> onFailure)
        {
            return result.IsSuccess ? onSuccess(result.Value!) : onFailure(result);
        }
    }

    extension(Result result)
    {
        public TOut Match<TOut>(
            Func<TOut> onSuccess,
            Func<Result, TOut> onFailure)
        {
            return result.IsSuccess ? onSuccess() : onFailure(result);
        }
    }
}