using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Types
{
    public class KarenEvent
    {
        event Action Event;
        public void AddListerner(Action action)
        {
            Event += action;
        }
        public async Task Wait()
        {
            bool w = false;
            Event += Unsub;
            while (!w)
            {
                await Task.Delay(50);
            }
            Event -= Unsub;
            void Unsub()
            {
                w = true;
            }
        }
        public void Invoke()
        {
            Event.Invoke();
        }
    }
}
