using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Validators;
using FluentValidation;
using SharedKernel.Command.Interfaces;
using SharedKernel.Repositories;

namespace Application.TodoItems.Commands
{
    public class UpdateTodoCommand : ICommand
    {
        public string ListId { get; set; } = string.Empty;
        public int TodoId { get; set; } = 0;

        public string Text { get; set; } = string.Empty;

        public bool Done { get; set; } = false;
    }

    public class UpdateTodoItemHandler : ICommandHandler<UpdateTodoCommand>
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly TodoItemValidator _validator;
        
        public UpdateTodoItemHandler(ITodoItemRepository todoItemRepository, TodoItemValidator validator)
        {
            _todoItemRepository = todoItemRepository;
            _validator = validator;
        }

        public async Task HandleAsync(UpdateTodoCommand command, CancellationToken token = default)
        {
            var entity = new TodoItem
            {
                TodoId = command.TodoId,
                Done = command.Done,
                Text = command.Text,
                ListId = command.ListId
            };
            
            await _validator.ValidateAndThrowAsync(entity, token);
            
            await _todoItemRepository.UpdateAsync(entity, token);
        }
    }
}