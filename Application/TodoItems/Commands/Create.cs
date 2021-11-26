using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using SharedKernel.Command.Interfaces;
using SharedKernel.Repositories;

namespace Application.TodoItems.Commands
{
    public class CreateTodoCommand : ICommand
    {
        public string ListId { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }

    public class CreateTodoItemHandler : ICommandHandler<CreateTodoCommand>
    {
        private readonly ITodoItemRepository _todoItemRepository;
        public CreateTodoItemHandler(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task HandleAsync(CreateTodoCommand command, CancellationToken token = default)
        {
            var entity = new TodoItem
            {
                ListId = command.ListId,
                Text = command.Text,
            };
            
            // await _todoItemValidator.ValidateAndThrowAsync(entity, token);

            await _todoItemRepository.AddAsync(entity, token);
        }
    }
}