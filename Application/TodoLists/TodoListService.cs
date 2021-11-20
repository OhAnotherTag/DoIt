using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.TodoLists.Handlers;
using SharedKernel.Interfaces;

namespace Application.TodoLists
{
    public class TodoListService : ITodoListService
    {
        private readonly CreateTodoListHandler _createTodoListHandler;
        private readonly UpdateTodoListHandler _updateTodoListHandler;
        private readonly DeleteTodoListHandler _deleteTodoListHandler;
        private readonly GetAllTodoListHandler _getAllTodoListHandler;
        private readonly GetByIdTodoListHandler _getByIdTodoListHandler;

        public TodoListService(ITodoListRepository todoListRepository)
        {
            _getByIdTodoListHandler = new GetByIdTodoListHandler(todoListRepository);
            _createTodoListHandler = new CreateTodoListHandler(todoListRepository);
            _updateTodoListHandler = new UpdateTodoListHandler(todoListRepository);
            _deleteTodoListHandler = new DeleteTodoListHandler(todoListRepository);
            _getAllTodoListHandler = new GetAllTodoListHandler(todoListRepository);
        }

        public Task<TodoListDto> GetByIdAsync(string request, CancellationToken token) =>
            _getByIdTodoListHandler.Handle(request, token);
        
        public Task<List<TodoListDto>> GetAllAsync(GetAllTodoListQuery request, CancellationToken token) =>
            _getAllTodoListHandler.Handle(request, token);

        public Task<string> CreateAsync(CreateTodoListDto item, CancellationToken token) =>
            _createTodoListHandler.Handle(item, token);

        public Task UpdateAsync(UpdateTodoListDto item, CancellationToken token) =>
            _updateTodoListHandler.Handle(item, token);

        public Task DeleteAsync(DeleteTodoListDto item, CancellationToken token) =>
            _deleteTodoListHandler.Handle(item, token);
    }
}