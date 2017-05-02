
namespace MVVMHeplers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class MyCommandWrapper : ICommand
    {
        public event EventHandler CanExecuteChanged 
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        Action<object> theCommand;
        Func<bool> canExecuteCondition;
        Func<object,bool> canExecuteOnObject;

        public MyCommandWrapper(Action<object> a, Func<bool> canExecute)
        {
            theCommand = a;
            canExecuteCondition = canExecute;
            canExecuteOnObject = (x) => true;
        } 

        public MyCommandWrapper(Action<object> a)
        {
            theCommand = a;
            canExecuteCondition = () => true;
        }

        public MyCommandWrapper(Action<object> a, Func<object,bool> canExecute)
        {
            theCommand = a;
            canExecuteCondition = null;
            canExecuteOnObject = canExecute;
        }

        //Can exexute if either the condition for execution is true
        //or if only an action is set
        public bool CanExecute(object parameter)
        {
            if (canExecuteCondition != null)
                return canExecuteCondition();

            if (canExecuteOnObject != null)
                return canExecuteOnObject(parameter);

            if (theCommand != null)
                return true;
 
            return false;

        }

        public void Execute(object parameter)
        {
            if (theCommand != null)
                theCommand(parameter);
        }
    }
}
