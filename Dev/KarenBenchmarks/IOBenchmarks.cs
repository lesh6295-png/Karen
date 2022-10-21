using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Pipes;
using System.IO;
using BenchmarkDotNet.Attributes;
namespace Karen.Benchmarks
{
    [MinColumn,MaxColumn,HtmlExporter,HardwareCounters]
    public partial class Benchmarks
    {
        static Benchmarks()
        {
        }
        [Benchmark]
        public void WriteText()
        {
            File.WriteAllText("iobenchmark_writetext", Karen.Types.Extensions.RandomString(max: 256));
        }
        [Benchmark]
        public void PipesConnectToServer()
        {
            var bench = Task.Factory.StartNew(() =>
           {
               try
               {
                   var token = new CancellationTokenSource();
                   var w = Task.Factory.StartNew(() =>
                   {
                       var server = new NamedPipeServerStream("benchmarkpipe");
                       server.WaitForConnection();
                       StreamReader reader = new StreamReader(server);
                       StreamWriter writer = new StreamWriter(server);
                       while (true)
                       {
                           token.Token.ThrowIfCancellationRequested();
                           var line = reader.ReadLine();
                           writer.WriteLine(String.Join("", line.Reverse()));
                           writer.Flush();
                       }
                   }, token.Token);
                   var q = new NamedPipeClientStream("benchmarkpipe");
                   q.Connect();
                   var wr = new StreamWriter(q);
                   wr.WriteLine(Karen.Types.Extensions.RandomString(max: 256));
                   wr.Flush();
                   Console.WriteLine(new StreamReader(q).ReadLine());
                   wr.Dispose();
                   token.Cancel();
                   q.Close();
                   q.Dispose();
               }
               catch { }
           });
            bench.Wait(5000);
        }
    }
}
