using System;
using System.Threading;
using System.Threading.Tasks;
using SharedKernel.Query.Interfaces;
using SharedKernel.Repositories;

namespace Application.TodoLists.Queries
{
    public class GetByIdTodoListQuery : IQuery<TodoListDto>
    {
        public string ListId { get; set; }
    }

    public class GetByIdTodoListHandler : IQueryHandler<GetByIdTodoListQuery, TodoListDto>
    {
        private readonly ITodoListRepository _todoListRepository;

        public GetByIdTodoListHandler(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        public async Task<TodoListDto> HandleAsync(GetByIdTodoListQuery query, CancellationToken token = default)
        {
            if (Guid.TryParse(query.ListId, out var guid))
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