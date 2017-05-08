using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDContainer.Model
{
    public class CRUDItem<T> : IEquatable<CRUDItem<T>> where T : IEquatable<T>,INotifyPropertyChanged
    {
        T _item;
        int _uniqueId;
        private bool _changed;

        public CRUDItem(T item)
        {
            _item = item;
            item.PropertyChanged += InternalChangeHander;
        }

        private void InternalChangeHander(object sender, PropertyChangedEventArgs e)
        { 
            _changed = true;
        }

        public bool Equals(CRUDItem<T> other)
        {
            return _item.Equals(other._item);
        }
    }
}
