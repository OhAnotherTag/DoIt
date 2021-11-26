using System.Threading;
using System.Threading.Tasks;

namespace SharedKernel.Query.Interfaces
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken token = default);
    }
}