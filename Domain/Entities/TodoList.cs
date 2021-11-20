using System.Collections.Generic;
using Domain.Validators;
using FluentValidation;

namespace Domain.Entities
{
    public class TodoList
    {
        public TodoList(string listId, string title)
        {
            ListId = listId;
            Title = title;
            
            var validator = new TodoListValidator();
            validator.ValidateAndThrow(this);
        }

        public readonly string ListId;
        public readonly string Title;
        public List<TodoItem> Items { get; private set; } = new List<TodoItem>();
    }
}