using NUnit.Framework;

namespace Karen.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void EventManager_AddAndCallEvent()
        {
            Karen.Engine.EventManager.AddEvent("test");
            Karen.Engine.EventManager.AddListerner("test", () => { Assert.Pass(); });
            Karen.Engine.EventManager.CallEvent("test");
        }

    }
}