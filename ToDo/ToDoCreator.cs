using System;

namespace ToDo
{
    public class ToDoCreator
    {
        readonly IToDoItemRepository repository;

        public ToDoCreator(IToDoItemRepository repository)
        {
            this.repository = repository;
        }

        public ToDoItem New(string text)
        {
            var item = new ToDoItem
            {
                ItemId = Guid.NewGuid().ToString(),
                ItemText = text,
                CreatedAt = DateTimeOffset.UtcNow
            };
            repository.Save(item);
            return item;
        }
    }
}
