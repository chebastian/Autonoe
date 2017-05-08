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
        public CRUDItemViewModel(CRUDItem<T> item)
        {
            TheItem = item; 
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
    }
}
