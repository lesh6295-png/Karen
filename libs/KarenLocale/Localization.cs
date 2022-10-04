using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
namespace Karen.Locale
{
    public static class Localization
    {
        static string targetCulture = "";
        static string defaultCulture = "en";
        public static string Culture
        {
            get
            {
                if (string.IsNullOrWhiteSpace(targetCulture))
                    return defaultCulture;
                return targetCulture;
            }
            set
            {
                targetCulture = value;
            }
        }
        public static string DefaultCulture
        {
            get
            {
                return defaultCulture;
            }

            set
            {
                defaultCulture = value;
            }
        }
        static Localization()
        {
            targetCulture = Thread.CurrentThread.CurrentUICulture.Name;

        }
    }
}
