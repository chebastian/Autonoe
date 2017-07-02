using MVVMHelpers;
using MVVMHeplers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;
using System.Windows.Media;

namespace HexView
{
    public class HighlightToBackgroundConverter : IValueConverter
    {
        public String TrueValue;
        public String FalseValue;

        public HighlightToBackgroundConverter()
        {
            TrueValue = "Red";
            FalseValue = "White";
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)(value) ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MarkedColorConverter : HighlightToBackgroundConverter
    {
        public MarkedColorConverter()
        {
            TrueValue = "Yellow";
            FalseValue = "White";
        }
    }

    public class MatrixRow : MVVMHelpers.ViewModelBase
    {
        private int _rownum;
        private int _rowsz;

        public MatrixRow()
            :base()
        {
            _rownum = 0;
            _rowsz = 16;
            Cells = new ObservableCollection<MatrixCell>();
        }

        public MatrixRow(ICellProvider provider, int row, int rowsz)
            :base()
        {
            _rownum = row;
            _rowsz = rowsz;
            Cells = new ObservableCollection<MatrixCell>();
            RowNumber = new ObservableField<int>(_rownum); 
            RowName = new ObservableField<string>(String.Format("{0}",RowNumber.Value.ToString("X4")));
             

            for (var i = 0; i < rowsz; i++)
                Cells.Add(new CellProxy(provider, (row * rowsz) + i)); 
        }

        public ObservableField<int> RowNumber { get; set; }
        public ObservableField<String> RowName { get; set; }

        public ObservableCollection<MatrixCell> Cells
        {
            get;set;
        }
    }

    public class MatrixCell  : MVVMHelpers.ViewModelBase
    {
        protected string _val;
        private string oldValue;
        private bool _highlight;
        private bool _marked;
        //private ObservableField<string> _myValue = new ObservableField<string>("FF");
        private string _myValue = String.Empty;

        public MatrixCell()
        {
            IsHighlighted = new ObservableField<bool>(false);
        }

        public virtual String GetMyValue()
        {
            return _val;
        }
 
        public ICommand CellClicked
        {
            get
            {
                return new MyCommandWrapper(x =>
                {
                    Marked = true;
                    UIEventaggregator<CellSelectedEvent>.Instance.Publish(new CellSelectedEvent(this));
                });
            }
        }

        public ICommand Highlight
        {
            get
            {
                return new MyCommandWrapper(x =>
                {
                    //oldValue = MyValue.Value;
                    //byte newValue = MyValue.Value;
                    //Byte.TryParse(oldValue,out newValue);
                   // MyValue.Set(String.Format("{0}",b));
                    IsHighlighted.Set(true); 
                });
            }
        }

        public ICommand UnHighlight
        {
            get
            {
                return new MyCommandWrapper(x =>
                {
                    //MyValue.Set(oldValue);
                    IsHighlighted.Set(false);
                });
            }
        }

        public ObservableField<bool> IsHighlighted
        {
            get;set;
        }

 
        public String MyValue
        {
            set {
                _myValue = value;
                SetPropertyChanged();
            }
            get => GetMyValue();
        }

        public bool Marked { get => _marked;
            set { _marked = value; SetPropertyChanged(); }
        }

        public Dictionary<char, byte> HexDict { get; private set; }
    }

    public class CellProxy : MatrixCell
    {
        private ICellProvider _provider;
        private int _offset;

        public CellProxy(ICellProvider provider, int offset)
        {
            _provider = provider;
            _offset = offset;
            //MyValue.Value = _provider.ReadCellValue(_offset).ToString("X2");
        }


        public override string GetMyValue()
        {
            var bytes = _provider.ReadCellValue(_offset);
            Console.WriteLine(_offset);

            return bytes.ToString("X2");
        }
    }
}
