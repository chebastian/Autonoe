using CRUDContainer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDContainer.ViewModel
{
    public class CRUDItemViewModel<T> where T: INotifyPropertyChanged , IEquatable<T>
    {
        CRUDItem<T> theItem;
        public CRUDItemViewModel(CRUDItem<T> item)
        {
            TheItem = item; 
        }

        public CRUDItem<T> TheItem { get => theItem;
            set
            {
                theItem = value;
                SetPropertyChanged();
            }
    }
}
