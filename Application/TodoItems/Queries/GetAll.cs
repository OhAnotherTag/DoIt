using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SharedKernel.Query.Interfaces;
using SharedKernel.Repositories;

namespace Application.TodoItems.Queries
{
    public class GetAllTodosQuery : IQuery<List<TodoDto>>
    {
        public string ListId { get; set; } = string.Empty;
    }

    public class GetTodoItemHandler : IQueryHandler<GetAllTodosQuery, List<TodoDto>>
    {
        private readonly ITodoItemRepository _todoItemRepository;
        public GetTodoItemHandler(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task<List<TodoDto>> HandleAsync(GetAllTodosQuery query, CancellationToken token = default)
        {
            var todos = await _todoItemRepository.ListAllAsync(query.ListId, token);
            return todos.Select(todo => new TodoDto
            {
                TodoId = todo.TodoId,
                ListId = todo.ListId,
                Text = todo.Text,
                Done = todo.Done
            }).ToList();
        }
    }
}