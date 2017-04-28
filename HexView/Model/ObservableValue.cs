using MVVMHelpers;

namespace HexView
{
    public class ObservableField<T> : ViewModelBase
    {
        public ObservableField(T theVal)
        {
            Value = theVal;
        }

        public T Set(T t)
        {
            Value = t;
            return Value;
        } 


        T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                SetPropertyChanged();
            }
        }
    }
}