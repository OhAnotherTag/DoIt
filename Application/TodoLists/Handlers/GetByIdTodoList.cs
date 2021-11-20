using System;
using System.Threading;
using System.Threading.Tasks;
using SharedKernel.Interfaces;

namespace Application.TodoLists.Handlers
{
    public class GetByIdTodoListQuery
    {
        
    }

    public class GetByIdTodoListHandler
    {
        private readonly ITodoListRepository _todoListRepository;

        public GetByIdTodoListHandler(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        public async Task<TodoListDto> Handle(string id, CancellationToken token)
        {
            if (Guid.TryParse(id, out var guid))
            {
                throw new ArgumentException("param ListId must be a valid Guid");
            }
            var todos = await _todoListRepository.GetByIdAsync(guid.ToString(), token);
            return new TodoListDto
            {
                ListId = todos.ListId,
                Title = todos.Title,
                Items = todos.Items
            } ;
        }
    }
}