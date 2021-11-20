using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.TodoLists.Handlers;

namespace Application.Common.Interfaces
{
    public interface ITodoListService
    {
        public Task<TodoListDto> GetByIdAsync(string request, CancellationToken token);
        public Task<List<TodoListDto>> GetAllAsync(GetAllTodoListQuery request, CancellationToken token);

        public Task<string> CreateAsync(CreateTodoListDto item, CancellationToken token);

        public Task UpdateAsync(UpdateTodoListDto item, CancellationToken token);

        public Task DeleteAsync(DeleteTodoListDto item, CancellationToken token);
    }
}