using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace SharedKernel.Interfaces
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> GetByIdAsync(int id, CancellationToken token);
        Task<List<TodoItem>> ListAllAsync(string listId, CancellationToken token);
        Task AddAsync(TodoItem item, CancellationToken token);
        Task UpdateAsync(TodoItem item, CancellationToken token);
        Task DeleteAsync(TodoItem item, CancellationToken token);
    }
}