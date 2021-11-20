namespace Domain.Entities
{
    public class TodoItem
    {
        public int TodoId { get; set; }
        public string ListId { get; set; }
        public string Text { get; set; }
        public bool Done { get; set; }
    }
}