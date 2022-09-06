using System;

namespace Karen
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.File.WriteAllText("message.txt", "HI!");
        }
    }
}
