namespace AspNetCoreTodo.Models
{
    public class TodoViewModel
    {
        public TodoItem[]? Items { get; set; }
        public TodoItem[]? CompleteItems { get; set; }
        public string[]? Header { get; set; }
    }
}