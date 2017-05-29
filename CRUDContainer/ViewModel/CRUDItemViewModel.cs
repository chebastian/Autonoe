using CRUDContainer.Model;
using MVVMHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDContainer.ViewModel
{
    public class CRUDItemViewModel<T> : ViewModelBase 
        where T: INotifyPropertyChanged , IEquatable<T>
    {
        CRUDItem<T> theItem;
        public int MyId;
        static int index = 0;

        String someParam;

        public CRUDItemViewModel(CRUDItem<T> item)
        {
            TheItem = item;
            MyId = index;
            index++;
        }

        public CRUDItemViewModel()
        { 
        }

        public CRUDItem<T> TheItem
        {
            get => theItem;
            set
            {
                theItem = value;
                SetPropertyChanged();
            }
        }

        public string SomeParam { get => someParam; set { someParam = value; SetPropertyChanged(); } }
    }

    public interface IViewModel : IEquatable<IViewModel>, INotifyPropertyChanged
    {

    }

    public class CrudFactory 
    {
        public static CRUDItem<T> Create<T>(T item) where T : IViewModel,IEquatable<T>
        {
            return new CRUDItem<T>(item);
        }

        public static CRUDItemViewModel<T> CreateVM<T>(T item) where T : IViewModel,IEquatable<T>
        {
            return new CRUDItemViewModel<T>(Create(item));
        } 
    }
}
