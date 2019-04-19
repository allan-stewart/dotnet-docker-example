using System;
using NUnit.Framework;

namespace ToDo.Tests
{
    public class ToDoCreatorTests : WithAnAutomockedTest<ToDoCreator>
    {
        [Test]
        public void When_creating_a_new_item()
        {
            var text = "Test text!";
            var before = DateTimeOffset.UtcNow;

            var result = classUnderTest.New(text);
            var after = DateTimeOffset.UtcNow;

            Assert.That(result, Is.Not.Null);
            Assert.That(Guid.TryParse(result.ItemId, out var guid), Is.True);
            Assert.That(result.ItemText, Is.EqualTo(text));
            Assert.That(result.CreatedAt, Is.GreaterThanOrEqualTo(before));
            Assert.That(result.CreatedAt, Is.LessThanOrEqualTo(after));
            Assert.That(result.CompletedAt, Is.Null);

            GetMock<IToDoItemRepository>().Verify(x => x.Save(result));
        }
    }
}