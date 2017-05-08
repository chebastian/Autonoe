using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRUDContainer.Model
{
    public class CRUDItemBase : IEquatable<CRUDItemBase>, INotifyPropertyChanged
    {
        private int _id;
        private String name;

        public string Name { get => name;
            set
            {
                name = value;
                SetPropertyChanged();
            }
        }

        public CRUDItemBase(int id)
        {
            _id = id;
            Name = "Base";
        } 

        public event PropertyChangedEventHandler PropertyChanged;
        public void SetPropertyChanged([CallerMemberName] String name = null)
        {
            if (PropertyChanged != null)
            {
                if (name != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public bool Equals(CRUDItemBase other)
        {
            return _id.Equals(other._id);
        }
    };
}
