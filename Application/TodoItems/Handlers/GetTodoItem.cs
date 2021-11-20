using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharedKernel.Interfaces;

namespace Application.TodoItems.Handlers
{
    public class GetTodoItemQuery
    {
        public int Id { get; set; }
    }

    public class GetTodoItemHandler
    {
        private readonly ITodoItemRepository _todoItemRepository;
        public GetTodoItemHandler(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }
        public async Task<List<TodoItemBriefDto>> Handle(string listId, CancellationToken token)
        {
            var todos = await _todoItemRepository.ListAllAsync(listId, token);
            return todos.Select(todo => new TodoItemBriefDto
            {
                TodoId = todo.TodoId,
                ListId = todo.ListId,
                Text = todo.Text
            }).ToList();
        }
    }
}