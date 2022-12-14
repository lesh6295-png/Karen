using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Engine.Api
{
    public static partial class Api
    {
        public static async Task waitevent(object?[]? par)
        {
            await EventManager.Singelton.Wait((string)par[0]);
        }
        public static async Task callevent(object?[]? par)
        {
            EventManager.Singelton.CallEvent((string)par[0]);
        }
        public static async Task addevent(object?[]? par)
        {
            EventManager.Singelton.AddEvent((string)par[0]);
        }
    }
}
