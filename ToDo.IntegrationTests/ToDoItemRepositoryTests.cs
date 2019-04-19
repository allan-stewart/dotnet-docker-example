using System;
using NUnit.Framework;

namespace ToDo.IntegrationTests
{
    public class ToDoItemRepositoryTests
    {
        private ToDoItemRepository classUnderTest;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var settings = new TestSettings();
            var connection = new PostgresConnection(settings.PostgresSettings);
            classUnderTest = new ToDoItemRepository(connection);
        }

        [Test]
        public void When_round_tripping_a_todo_item()
        {
            var item = new ToDoItem
            {
                ItemId = Guid.NewGuid().ToString(),
                ItemText = "Test round-trip integration",
                CreatedAt = DateTimeOffset.UtcNow.AddDays(-1),
                CompletedAt = null
            };

            classUnderTest.Save(item);
            var result = classUnderTest.Find(item.ItemId);

            Assert.That(result.ItemText, Is.EqualTo(item.ItemText));
            Assert.That(result.CreatedAt, Is.EqualTo(item.CreatedAt));
            Assert.That(result.CompletedAt, Is.Null);
        }
    }
}
