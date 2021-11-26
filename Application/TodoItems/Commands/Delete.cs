using System;
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
        public string ListId { get; set; }
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
            if (!Guid.TryParse(command.ListId, out _))
            {
                throw new ArgumentException("ListId must be a Guid");
            }
            
            var entity = new TodoItem
            {
                TodoId = command.TodoId,
                ListId = command.ListId
            };

            await _todoItemRepository.DeleteAsync(entity, token);
        }
    }
}