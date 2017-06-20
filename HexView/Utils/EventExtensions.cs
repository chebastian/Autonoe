using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HexView.Utils
{
    public static class EventExtensions
    {
        public static void LogPublish<EventType>(this EventType evt , EventType args, [CallerFilePath] string file = null) where EventType : PubSubEvent<EventType>,new()
        { 
            evt.Publish(args); 
        }
    }
}
