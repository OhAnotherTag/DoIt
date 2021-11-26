using System;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class TodoListValidator : AbstractValidator<TodoList>
    {
        public TodoListValidator()
        {
            RuleFor(list => list.ListId).MustBeAGuid();
            RuleFor(list => list.Title).MinimumLength(3).MaximumLength(50);
            // RuleFor(list => list.Items).NotNull();
        }
    }
}