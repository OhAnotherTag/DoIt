using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Validators;
using FluentValidation;
using SharedKernel.Interfaces;

namespace Application.TodoItems.Handlers
{
    public class CreateTodoItemDto
    {
        public string ListId { get; set; }
        public string Text { get; set; }
    }

    public class CreateTodoItemHandler
    {
        private readonly ITodoItemRepository _todoItemRepository;
        public CreateTodoItemHandler(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task Handle(CreateTodoItemDto request, CancellationToken token)
        {
            var entity = new TodoItem
            {
                ListId = request.ListId,
                Text = request.Text,
            };
            
            // await _todoItemValidator.ValidateAndThrowAsync(entity, token);

            await _todoItemRepository.AddAsync(entity, token);
        }
    }
}