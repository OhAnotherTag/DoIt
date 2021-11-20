using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class TodoItemValidator : AbstractValidator<TodoItem>
    {
        public TodoItemValidator()
        {
            RuleFor(item => item.TodoId).GreaterThanOrEqualTo(0).NotNull();
            RuleFor(item => item.Text).MinimumLength(3).MaximumLength(50).NotNull();
            RuleFor(item => item.ListId).MustBeAGuid();
        }
    }
}