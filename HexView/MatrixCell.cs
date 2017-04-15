using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexView
{

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

        public virtual String GetMyValue()
        {
            return _val;
        }

        public bool IsHighlighted
        {
            get => _highlight;
            set => _highlight = value;
        }

        public String MyValue
        {
            get
            {
                return GetMyValue();
            }
            set
            {
                _val = value;
                SetPropertyChanged();
            }
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
