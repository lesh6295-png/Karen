using System.Threading.Tasks;
using Karen.Types;

namespace Karen.Engine.Api
{
    internal static partial class Api
    {

        public static async Task say(object?[]? par)
        {
            string text = par.TryExtractElement<object, string>("unk");

            await MainWindow.Singelton.WriteText(text);
        }
    }
}