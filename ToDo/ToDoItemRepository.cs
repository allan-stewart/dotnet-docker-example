using System;
using System.Linq;

namespace ToDo
{
    public interface IToDoItemRepository
    {
        void Save(ToDoItem item);
        ToDoItem Find(string itemId);
        void Delete(string itemId);
    }

    public class ToDoItemRepository
    {
        readonly IPostgresConnection connection;
        
        public ToDoItemRepository(IPostgresConnection connection)
        {
            this.connection = connection;
        }

        public void Save(ToDoItem item)
        {
            var statement = "INSERT INTO todo_items (item_id, item_text, created_at, completed_at) VALUES (@ItemId, @ItemText, @CreatedAt, @CompletedAt);";
            connection.WriteData(statement, item);
        }

        public ToDoItem Find(string itemId)
        {
            var query = "SELECT item_id, item_text, created_at, completed_at FROM todo_items WHERE item_id = @itemId";
            return connection.ReadData<ToDoItem>(query, new { itemId }).FirstOrDefault();
        }

        public void Delete(string itemId)
        {
            var statement = "DELETE FROM todo_items WHERE item_id = @itemId";
            connection.WriteData(statement, new { itemId });
        }
    }
}
