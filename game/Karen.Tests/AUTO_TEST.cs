#pragma warning disable IDE0035
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NUnit.Framework;
using System.IO;
namespace Karen.Tests
{
    public class AUTO_TEST
    {
        [Test]
        public void Auto()
        {
#if DEBUG
            Assert.Fail("Testing configuration required!");
            return;
#endif
#if RELEASE
            Assert.Fail("Testing configuration required!");
            return;
#endif

            TestContext.Progress.WriteLine($"Start AUTO_TEST\nWorking path: {Environment.CurrentDirectory}");
            string exepath = $"\\..\\..\\..\\..\\..\\bin\\Karen\\Testing\\{Environment.CurrentDirectory.Split("\\").Last()}";
            Environment.CurrentDirectory += exepath;
            TestContext.Progress.WriteLine($"Karen path: {Environment.CurrentDirectory}");
            File.Delete("lasterror.log");
            Process karenGame = new Process();
            karenGame.StartInfo.FileName = "Karen.exe";
            karenGame.StartInfo.Arguments = "--testing";
            

            karenGame.Start();
            TestContext.Progress.WriteLine($"Process started with {karenGame.Id} id.");
            while (!karenGame.WaitForExit(750))
            {
                karenGame.Refresh();
                Task.Delay(50).Wait();
            }
            karenGame.Refresh();
            TestContext.Progress.WriteLine($"Stop code: {karenGame.ExitCode}");
#if TESTING
            TestContext.Progress.WriteLine(Karen.Registry.RegController.GetExcRes());
#endif
            //TODO: import lasterror.log
            /* if (File.Exists("lasterror.log"))
             {
                 Assert.Fail($"Karen AUTO_TEST fall: lasterror: {File.ReadAllText("lasterror.log")}");
             }*/
            if (karenGame.ExitCode != 0)
            {
                Assert.Fail($"Unknown fall: Exit code: {karenGame.ExitCode}");
            }
            Assert.Pass("Karen AUTO_TEST pass!");
        }
    }
}
