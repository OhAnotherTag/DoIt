using System.Collections.Generic;
using Domain.Validators;
using FluentValidation;

namespace Domain.Entities
{
    public class TodoList
    {
        public string ListId { get; set; }
        public string Title { get; set; }
        public List<TodoItem> Items { get; private set; } = new List<TodoItem>();
    }
}