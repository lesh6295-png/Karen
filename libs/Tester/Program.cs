using System;
namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Karen.Locale.KeySourceFactory.GenerateTestSource("test.locale");
            var q = new Karen.Locale.KeySource("2.locale");
            Console.WriteLine(Karen.Locale.Localization.Culture);
            Console.WriteLine(q.TryExtractTranslate("karen_clothes"));
            Console.ReadKey();
        }
    }
}
