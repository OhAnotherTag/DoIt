using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharedKernel.Interfaces
{
    public interface IAsyncRepository<T>
    {
        Task<T> GetByIdAsync(int id, CancellationToken token);
        Task<List<T>> ListAllAsync(string listId, CancellationToken token);
        Task AddAsync(T item, CancellationToken token);
        Task UpdateAsync(T item, CancellationToken token);
        Task DeleteAsync(T item, CancellationToken token);
    }
}