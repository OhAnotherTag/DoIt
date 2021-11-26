using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Validators;
using FluentValidation;
using SharedKernel.Command.Interfaces;
using SharedKernel.Repositories;

namespace Application.TodoLists.Commands
{
    public class CreateTodoListCommand : ICommand
    {
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
    }
    
    public class CreateTodoListHandler : ICommandHandler<CreateTodoListCommand>
    {
        private readonly ITodoListRepository _todoListRepository;
        private readonly TodoListValidator _validator;

        public CreateTodoListHandler(ITodoListRepository todoListRepository, TodoListValidator validator)
        {
            _todoListRepository = todoListRepository;
            _validator = validator;
        }

        public async Task HandleAsync(CreateTodoListCommand command, CancellationToken token = default)
        {
            var id = Guid.NewGuid().ToString();
            var entity = new TodoList
            {
                ListId = id,
                Title = command.Title
            };
            
            await _validator.ValidateAndThrowAsync(entity, token);

            await _todoListRepository.AddAsync(entity, token);
        }
    }
}