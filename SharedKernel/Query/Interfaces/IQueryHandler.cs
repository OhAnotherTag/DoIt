using System.Threading;
using System.Threading.Tasks;

namespace SharedKernel.Query.Interfaces
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : class, IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken token = default);
    }
}