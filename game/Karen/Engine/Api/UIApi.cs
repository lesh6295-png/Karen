using System.Threading.Tasks;
using Karen.Types;
using Karen.KBL;
using System.Collections.Generic;
using System.Linq;
using Karen.Engine.Scripting;
using Karen.Locale;
namespace Karen.Engine.Api
{
    public static partial class Api
    {
        public static async Task waitui(object?[]? par)
        {
            //wait for mainwindow add himself in singelton filed
            while (MainWindow.Singelton == null)
            {
                await Task.Delay(20);
            }

            while (!MainWindow.Singelton.IsReady)
            {
                await Task.Delay(50);
            }
        }
        public static async Task say(object?[]? par)
        {
            //TODO: change this
            await waitui(null);

            string text = par.TryExtractElement<object, string>("unk");
            MainWindow.Singelton.HideWindow = false;
            await MainWindow.Singelton.WriteText(SourceManager.Singelton.ExtractTranslate(text));
#if TESTING
            if (App.AUTO_TEST)
                return;
#endif
            while (MainWindow.Singelton.Next)
            {
                await Task.Delay(100);
            }
        }
        public static async Task print(object?[]? par)
        {

            //TODO: change this
            await waitui(null);


            string text = par.TryExtractElement<object, string>("unk");
            MainWindow.Singelton.HideWindow = false;
            await MainWindow.Singelton.WriteText(text);
#if TESTING
            if (Config.AUTO_TEST)
                return;
#endif
            while (MainWindow.Singelton.Next)
            {
                await Task.Delay(100);
            }
        }
        public static async Task sprite(object?[]? par)
        {
            //TODO: change this
            await waitui(null);

            string type = par.TryExtractElement<object, string>("body");
            int lib = par.TryExtractElement<object, int>(1, 1);
            int file = par.TryExtractElement<object, int>(1, 2);
            //WAIT INIS BEFORE CALL
            switch (type)
            {
                case "body":
                    MainWindow.Singelton.SetBodySprite(BinaryManager.Singelton.Extract(lib, file));
                    break;
                case "emotion":
                    MainWindow.Singelton.SetEmotionSprite(BinaryManager.Singelton.Extract(lib, file));
                    break;
            }
        }

        public static async Task select(object?[]? par)
        {

            //TODO: change this
            await waitui(null);


            List<string> keys = new(), endpoints = new();
            List<int> id = new List<int>();
            for (int i = 0; i < par.Length - 1; i += 2)
            {
                keys.Add((string)par[i]);
                endpoints.Add((string)par[i + 1]);
                id.Add(id.Count + 1);
            }
            var text = keys.Select((x) => { return SourceManager.Singelton.ExtractTranslate(x); }).ToArray();
            int result = 0;
            //TODO: Update select behaviour with AUTO_TEST
#if TESTING
            if (Config.AUTO_TEST)
                result=1;
            else
#endif
            result = await MainWindow.Singelton.Select(text, id.ToArray());
            ((ScriptContext)(((object[])par.Last())[3])).ToLabel(endpoints[result - 1]);
        }
    }
}