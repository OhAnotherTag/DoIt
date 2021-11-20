using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using SharedKernel.Interfaces;

namespace Application.TodoItems.Handlers
{
    public class UpdateTodoItemDto
    {
        public string ListId { get; set; }
        public int TodoId { get; set; }

        public string Text { get; set; }

        public bool Done { get; set; }
    }

    public class UpdateTodoItemHandler
    {
        private readonly ITodoItemRepository _todoItemRepository;
        public UpdateTodoItemHandler(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }
        public async Task Handle(UpdateTodoItemDto request, CancellationToken token)
        {
            var entity = new TodoItem
            {
                TodoId = request.TodoId,
                Done = request.Done,
                Text = request.Text
            };
            
            await _todoItemRepository.UpdateAsync(entity, token);
        }
    }
}