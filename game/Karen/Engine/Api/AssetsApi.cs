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
            switch (mode)
            {
                case "load":
                    string path = par.TryExtractElement<object, string>("unk", 1);
                    SourceManager.LoadSource(path);
                    break;
                case "unload":
                    int unloadid = par.TryExtractElement<object, int>(-1, 1);
                    SourceManager.UnloadSource(unloadid);
                    break;
                default:
                    throw new InvalidApiParamsException("Unknown locales mode: " + mode);
                    break;
            }
            Logger.Write($"SourceManager {mode} {par[1].ToString()}");
        }

        public static async Task kbl(object?[]? par)
        {
            string type = par.TryExtractElement<object, string>("unk");
            switch (type)
            {
                case "load":
                    string path = par.TryExtractElement<object, string>("main.miku", 1);
                    BinaryManager.Singelton.LoadKBL(path);
                    break;
                case "unload":
                    int id = par.TryExtractElement<object, int>(1, 1);
                    BinaryManager.Singelton.UnloadKBL(id);
                    break;
            }
        }
    }
}
