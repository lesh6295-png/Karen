using System.Threading.Tasks;
using Karen.Types;

namespace Karen.Engine.Api
{
    internal static partial class Api
    {

        public static async Task say(object?[]? par)
        {
            string text = par.TryExtractElement<object, string>("unk");
            MainWindow.Singelton.Hide = false;
            await MainWindow.Singelton.WriteText(text);
            while (MainWindow.Singelton.Next)
            {
                await Task.Delay(100);
            }
        }
    }
}