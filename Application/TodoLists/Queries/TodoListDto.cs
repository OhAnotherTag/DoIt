using System.Collections.Generic;
using Domain.Entities;

namespace Application.TodoLists.Queries
{
    public class TodoListDto
    {
        public string ListId { get; set; }
        public string Title { get; set; }
        public List<TodoItem> Items { get; set; }
    }
}