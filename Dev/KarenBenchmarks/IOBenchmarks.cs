using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Pipes;
using System.IO;
using BenchmarkDotNet.Attributes;
namespace Karen.Benchmarks
{
    [MinColumn,MaxColumn,HtmlExporter,HardwareCounters]
    public partial class Benchmarks
    {
        [Benchmark]
        public void WriteText()
        {
            File.WriteAllText("iobenchmark_writetext", Karen.Types.Extensions.RandomString(max: 256));
        }
    }
}
