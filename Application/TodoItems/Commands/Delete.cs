using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using SharedKernel.Command.Interfaces;
using SharedKernel.Repositories;

namespace Application.TodoItems.Commands
{
    public class DeleteTodoCommand : ICommand
    {
        public int TodoId { get; set; }
    }

    public class DeleteTodoItemHandler : ICommandHandler<DeleteTodoCommand>
    {
        private readonly ITodoItemRepository _todoItemRepository;
        
        public DeleteTodoItemHandler(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task HandleAsync(DeleteTodoCommand command, CancellationToken token = default)
        {
            var entity = new TodoItem
            {
                TodoId = command.TodoId
            };

            await _todoItemRepository.DeleteAsync(entity, token);
        }
    }
}