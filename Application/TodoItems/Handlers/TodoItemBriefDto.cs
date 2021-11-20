namespace Application.TodoItems.Handlers
{
    public class TodoItemBriefDto
    {
        public int TodoId { get; set; }

        public string ListId { get; set; }

        public string Text { get; set; }

        public bool Done { get; set; }
    }
}