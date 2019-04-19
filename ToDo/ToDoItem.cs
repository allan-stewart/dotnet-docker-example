using System;

namespace ToDo
{
    public class ToDoItem
    {
        public string ItemId { get; set; }
        public string ItemText { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
    }
}
