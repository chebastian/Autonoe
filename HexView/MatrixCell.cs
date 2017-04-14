using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNamespace
{
    public class MatrixCell  : MVVMHelpers.ViewModelBase
    {
        protected string _val;

        public virtual String GetMyValue()
        {
            return _val;
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
            var bytes = _provider.ReadCellValues(_offset, 16);
            var res = bytes.ToList().Skip(_offset).Take(16)
                .Select(x => x.ToString("X2")).Aggregate<String>((x, y) => x + " " + y);

            return res;
        }
    }
}
