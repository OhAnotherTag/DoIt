using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.TodoItems.Handlers;
using SharedKernel.Interfaces;

namespace Application.TodoItems
{
    public class TodoItemService : ITodoItemService
    {
        private readonly CreateTodoItemHandler _createTodoItemHandler;
        private readonly UpdateTodoItemHandler _updateTodoItemHandler;
        private readonly DeleteTodoItemHandler _deleteTodoItemHandler;
        private readonly GetTodoItemHandler _getTodoItemHandler;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _createTodoItemHandler = new CreateTodoItemHandler(todoItemRepository);
            _updateTodoItemHandler = new UpdateTodoItemHandler(todoItemRepository);
            _deleteTodoItemHandler = new DeleteTodoItemHandler(todoItemRepository);
            _getTodoItemHandler = new GetTodoItemHandler(todoItemRepository);
        }

        public Task<List<TodoItemBriefDto>> GetAllAsync(string listId, CancellationToken token) =>
            _getTodoItemHandler.Handle(listId, token);
        
        public Task CreateAsync(CreateTodoItemDto item, CancellationToken token) =>
            _createTodoItemHandler.Handle(item, token);
        
        public Task UpdateAsync(UpdateTodoItemDto item, CancellationToken token) =>
            _updateTodoItemHandler.Handle(item, token);

        public Task DeleteAsync(DeleteTodoItemDto item, CancellationToken token) =>
            _deleteTodoItemHandler.Handle(item, token);
    }
}