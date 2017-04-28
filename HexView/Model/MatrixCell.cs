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

            for (var i = 0; i < rowsz; i++)
                Cells.Add(new CellProxy(provider, (row * rowsz) + i));

        }

        public ObservableCollection<MatrixCell> Cells
        {
            get;set;
        }
    }

    public class MatrixCell  : MVVMHelpers.ViewModelBase
    {
        protected string _val;
        private bool _highlight;
        private bool _marked;
        private ObservableField<string> _myValue = new ObservableField<string>("FF");

        public MatrixCell()
        {
            MyValue = new ObservableField<string>("FA");
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
                    //IsHighlighted = true;
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
                    //IsHighlighted = false;
                    IsHighlighted.Set(false);
                });
            }
        }

        public ObservableField<bool> IsHighlighted
        {
            get;set;
        }

        //public bool IsHighlighted
        //{
        //    get => _highlight;
        //    set
        //    {
        //        _highlight = value;
        //        SetPropertyChanged();
        //    }
        //}
 
        //public String MyValue
        public ObservableField<String> MyValue
        {
            //get
            //{
            //    //return new ObservableValue<string>( GetMyValue() );
            //    return _myValue;
            //}
            //set
            //{
            //    _myValue = value;
            //    //SetPropertyChanged();
            //}
            set;get;
        }

        public bool Marked { get => _marked;
            set { _marked = value; SetPropertyChanged(); }
        }
    }

    public class CellProxy : MatrixCell
    {
        private ICellProvider _provider;
        private int _offset;

        public CellProxy(ICellProvider provider, int offset)
        {
            _provider = provider;
            _offset = offset;
            MyValue.Value = _provider.ReadCellValue(_offset).ToString("X2");
        }


        public override string GetMyValue()
        {
            var bytes = _provider.ReadCellValue(_offset);
            //var res = bytes.ToList().Skip(_offset).Take(16)
            //    .Select(x => x.ToString("X2")).Aggregate<String>((x, y) => x + " " + y);

            return bytes.ToString("X2");
        }
    }
}
