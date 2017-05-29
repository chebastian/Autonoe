using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCore.Events
{
    [Export(typeof(IEventAggregator))]
    public class EvtAgg : EventAggregator
    {
    }
}
