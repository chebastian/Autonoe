using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMHelpers
{
    public enum ViewModelMessages { ItemSelected = 1, NavChanged = 2, OpenDialog = 3, ProjectSave = 4, DialogCancel = 5};
    public class Mediator
    {
        static readonly Mediator instance = new Mediator();
        private volatile object locker = new object(); 
        MultiDictionary<ViewModelMessages,Action<Object>> intlist =
            new MultiDictionary<ViewModelMessages, Action<Object>>();

        static Mediator()
        {

        }

        private Mediator()
        {

        }

        public static Mediator Instance
        {
            get
            {
                return instance;
            }
        }

        public void RegisterActionOnMsg(Action<Object> action,ViewModelMessages msg)
        {
            intlist.AddValue(msg,action);
        }

        public void Notify(ViewModelMessages msg, object args)
        {
            if (intlist.ContainsKey(msg))
            {
                foreach (var callback in intlist[msg])
                    callback(args);
            }
        }

    }
}
