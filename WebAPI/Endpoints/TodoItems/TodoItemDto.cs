namespace WebAPI.Endpoints.TodoItems
{
    public class TodoItemDto
    {
        public int TodoId { get; set; }
        public string ListId { get; set; }
        public string Text { get; set; }
        public bool Done { get; set; }
    }
}