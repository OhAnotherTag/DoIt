using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class TodoItemValidator : AbstractValidator<TodoItem>
    {
        public TodoItemValidator()
        {
            RuleFor(item => item.TodoId).GreaterThanOrEqualTo(1);
            RuleFor(item => item.Text).MinimumLength(3).MaximumLength(50);
            RuleFor(item => item.ListId).MustBeAGuid();
        }
    }
}