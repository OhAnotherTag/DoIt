using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace SharedKernel.Repositories
{
    public interface ITodoListRepository
    {
        Task<TodoList> GetByIdAsync(string id, CancellationToken token);
        Task<List<TodoList>> ListAllAsync(int page, int limit, CancellationToken token);
        Task AddAsync(TodoList item, CancellationToken token);
        Task UpdateAsync(TodoList item, CancellationToken token);
        Task DeleteAsync(TodoList item, CancellationToken token);
        Task<List<TodoList>> ListAllWithTodosAsync(int page, int limit, CancellationToken token);
    }
}