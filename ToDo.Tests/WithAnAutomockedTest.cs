using AutoMoqCore;
using Moq;
using NUnit.Framework;

namespace ToDo.Tests
{
    public class WithAnAutomockedTest<T> where T : class
    {
        private AutoMoqer autoMocker;

        [SetUp]
        public void AutoMockSetup()
        {
            autoMocker = new AutoMoqer();
        }
        
        protected T classUnderTest => autoMocker.Create<T>();

        protected Mock<U> GetMock<U>() where U : class
        {
            return autoMocker.GetMock<U>();
        }

        protected TAny Any<TAny>()
        {
            return It.IsAny<TAny>();
        }
    }
}