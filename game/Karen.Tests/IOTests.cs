using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Karen.Engine.Api;
using System.IO;
namespace Karen.Tests
{
    public class IOTests
    {
        [Test]
        public void Current_Dir()
        {
            Console.WriteLine(Environment.CurrentDirectory);
        }
    }
}
