using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using SharedKernel.Interfaces;

namespace Application.TodoItems.Handlers
{
    public class DeleteTodoItemDto
    {
        public int Id { get; set; }
    }

    public class DeleteTodoItemHandler
    {
        private readonly ITodoItemRepository _todoItemRepository;
        public DeleteTodoItemHandler(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }
        public async Task Handle(DeleteTodoItemDto request, CancellationToken token)
        {
            var entity = new TodoItem
            {
                TodoId = request.Id
            };

            await _todoItemRepository.DeleteAsync(entity, token);
        }
    }
}