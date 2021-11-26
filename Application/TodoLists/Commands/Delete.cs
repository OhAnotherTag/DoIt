using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using SharedKernel.Command.Interfaces;
using SharedKernel.Repositories;

namespace Application.TodoLists.Commands
{
    public class DeleteTodoListCommand : ICommand
    { 
        public string ListId { get; set; } = string.Empty;
    }

    public class DeleteTodoListHandler : ICommandHandler<DeleteTodoListCommand>
    {
        private readonly ITodoListRepository _todoListRepository;

        public DeleteTodoListHandler(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        public async Task HandleAsync(DeleteTodoListCommand command, CancellationToken token = default)
        {
            if (!Guid.TryParse(command.ListId, out _))
            {
                throw new ArgumentException("ListId must be a Guid");
            }
            
            var entity = new TodoList
            {
                ListId = command.ListId
            };
            
            await _todoListRepository.DeleteAsync(entity, token);
        }
    }
}