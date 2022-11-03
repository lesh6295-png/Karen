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
        public void FallTest()
        {
            Assert.Fail();
        }
    }
}