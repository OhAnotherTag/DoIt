using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Endpoints.Common
{
    public abstract class BaseAsyncEndpoint<TRequest, TResponse> : BaseEndpoint
    {
        public abstract Task<ActionResult<TResponse>> HandleAsync(TRequest request, CancellationToken token);
    }
}