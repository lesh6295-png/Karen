using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
namespace Karen.Tests
{
    public class AUTO_TEST
    {
        [Test]
        public void Auto()
        {
            TestContext.Progress.WriteLine($"Start AUTO_TEST\nWorking path: {Environment.CurrentDirectory}");
        }
    }
}
