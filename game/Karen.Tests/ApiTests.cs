using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Karen.Engine.Api;
namespace Karen.Tests
{
    public class ApiTests
    {
        [Test]
        public void Api_Wait()
        {
            Api.wait(new object[] { 500 }).Wait();
            Console.WriteLine("Wait time: 0.500 seconds.");
            Assert.Pass();
        }
        [Test]
        public void WriteRegistry()
        {
            Karen.Registry.RegController.WriteState(Types.ClientState.Installed);
        }
        [Test]
        public void ReadRegistry()
        {
            var q = Karen.Registry.RegController.GetState();
        }
    }
}
