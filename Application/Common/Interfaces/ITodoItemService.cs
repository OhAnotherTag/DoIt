using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.TodoItems.Handlers;

namespace Application.Common.Interfaces
{
    public interface ITodoItemService
    {
        public Task<List<TodoItemBriefDto>> GetAllAsync(string listId, CancellationToken token);

        public Task CreateAsync(CreateTodoItemDto item, CancellationToken token);

        public Task UpdateAsync(UpdateTodoItemDto item, CancellationToken token);

        public Task DeleteAsync(DeleteTodoItemDto item, CancellationToken token);
    }
}