using Karen.Types;
using Karen.Locale;
using Karen.KBL;
using System.Threading.Tasks;

namespace Karen.Engine.Api
{
    public static partial class Api
    {
        public static async Task locales(object?[]? par)
        {
            string mode = par.TryExtractElement<object, string>("unk");
            string path;
            switch (mode)
            {
                case "load":
                    path = par.TryExtractElement<object, string>("unk", 1);
                    SourceManager.Singelton.LoadSource(path);
                    break;
                case "load_unsafe":
                    path = par.TryExtractElement<object, string>("unk", 1);
                    int q = SourceManager.Singelton.LoadSourceUnsafe(path);
                    if (q == -1)
                        Logger.Write("Locales unsafe load falied!");
                    break;
                case "unload":
                    int unloadid = par.TryExtractElement<object, int>(-1, 1);
                    SourceManager.Singelton.UnloadSource(unloadid);
                    break;
                default:
                    throw new InvalidApiParamsException("Unknown locales mode: " + mode);
                    break;
            }
            //Logger.Write($"SourceManager {mode} {par[1].ToString()}");
        }

        public static async Task kbl(object?[]? par)
        {
            string type = par.TryExtractElement<object, string>("unk");
            string path;
            switch (type)
            {
                case "load":
                    path = par.TryExtractElement<object, string>("main.miku", 1);
                    BinaryManager.Singelton.LoadKBL(path);
                    break;
                case "load_unsafe":
                    path = par.TryExtractElement<object, string>("main.miku", 1);
                    int q = BinaryManager.Singelton.LoadKBLUnsafe(path);
                    if (q == -1)
                    {
                        Logger.Write("Locales unsafe load falied!");
                    }
                    break;
                case "unload":
                    int id = par.TryExtractElement<object, int>(1, 1);
                    BinaryManager.Singelton.UnloadKBL(id);
                    break;
            }
        }
    }
}
