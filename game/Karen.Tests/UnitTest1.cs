using NUnit.Framework;
using Karen.Types;
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
            Assert.Fail();
        }

        [Test]
        public void EventManager_AddAndCallEvent()
        {
            var e = new Karen.Engine.EventManager();
            e.AddEvent("test");
            e.AddListerner("test", () => { Assert.Pass(); });
            e.CallEvent("test");
        }

        [Test]
        public void DynamicSerializator_SerialazeInt()
        {
            dynamic x = 5;
            string res = DynamicSerializator.ToString(x);
            Assert.AreEqual("int:5", res);
        }
        [Test]
        public void DynamicSerializator_DeserialazeInt()
        {
            string x = "int:1238";
            dynamic res = DynamicSerializator.FromString(x);
            Assert.AreEqual(1238, res);
        }
    }
}