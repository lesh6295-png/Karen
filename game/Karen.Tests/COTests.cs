using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace Karen.Tests
{
    public class COTests
    {
            [Test]
            public void To36System_0()
            {
                Assert.AreEqual("0", Karen.CO.NameShorter.get36sys(0));
            }
            [Test]
            public void To36System_1()
            {
                Assert.AreEqual("1", Karen.CO.NameShorter.get36sys(1));
            }
            [Test]
            public void To36System_34()
            {
                Assert.AreEqual("y", Karen.CO.NameShorter.get36sys(34));
            }
            [Test]
            public void To36System_34094399()
            {
                Assert.AreEqual("karen", Karen.CO.NameShorter.get36sys(34094399));
            }
            [Test]
            public void To36System_1626557542()
            {
                Assert.AreEqual("qwerty", Karen.CO.NameShorter.get36sys(1626557542));
            }
            [Test]
            public void To36System_17893382()
            {
                Assert.AreEqual("anime", Karen.CO.NameShorter.get36sys(17893382));
            }
            [Test]
            public void To36System_43152147()
            {
                Assert.AreEqual("power", Karen.CO.NameShorter.get36sys(43152147));
            }
            [Test]
            public void To36System_1296()
            {
                Assert.AreEqual("100", Karen.CO.NameShorter.get36sys(1296));
            }
        
    }
}