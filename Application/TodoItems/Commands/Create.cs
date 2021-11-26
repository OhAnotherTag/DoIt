using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Validators;
using FluentValidation;
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
        private readonly TodoItemValidator _validator;
        public CreateTodoItemHandler(ITodoItemRepository todoItemRepository, TodoItemValidator validator)
        {
            _todoItemRepository = todoItemRepository;
            _validator = validator;
        }

        public async Task HandleAsync(CreateTodoCommand command, CancellationToken token = default)
        {
            var entity = new TodoItem
            {
                ListId = command.ListId,
                Text = command.Text,
            };
            
            await _validator.ValidateAndThrowAsync(entity, token);

            await _todoItemRepository.AddAsync(entity, token);
        }
    }
}