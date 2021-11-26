using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Validators;
using FluentValidation;
using SharedKernel.Command.Interfaces;
using SharedKernel.Repositories;

namespace Application.TodoLists.Commands
{
    public class UpdateTodoListCommand : ICommand
    {
        public string ListId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
    
    public class UpdateTodoListHandler : ICommandHandler<UpdateTodoListCommand>
    {
        private readonly ITodoListRepository _todoListRepository;
        private readonly TodoListValidator _validator;

        public UpdateTodoListHandler(ITodoListRepository todoListRepository, TodoListValidator validator)
        {
            _todoListRepository = todoListRepository;
            _validator = validator;
        }

        public async Task HandleAsync(UpdateTodoListCommand command, CancellationToken token = default)
        {
            var entity = new TodoList
            {
                ListId = command.ListId,
                Title = command.Title
            };
            
            await _validator.ValidateAndThrowAsync(entity, token);

            await _todoListRepository.UpdateAsync(entity, token);
        }
    }
}