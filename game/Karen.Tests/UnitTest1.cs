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
            var e = new Karen.Engine.EventManager();
            e.AddEvent("test");
            e.AddListerner("test", () => { Assert.Pass(); });
            e.CallEvent("test");
        }

    }
}