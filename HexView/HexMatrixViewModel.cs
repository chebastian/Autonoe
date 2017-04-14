using MVVMHelpers;
using MyNamespace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexView
{
    public class HexMatrixViewModel : ViewModelBase, ICellProvider
    {
        private ObservableCollection<MatrixCell> _matrix;
        private Dictionary<byte, String> tokens = new Dictionary<byte, string>();
        private byte[] bytes;
        private FileStream _stream;

        public HexMatrixViewModel()
        {
            init();
            loadBytes(new byte[] { 255, 255, 123 });
        } 

        public HexMatrixViewModel(byte[] bytes)
        {
            this.bytes = bytes;
            init();

            loadBytes(bytes);
        }

        public HexMatrixViewModel(FileStream stream)
        {
            init();
            _stream = stream;

            for (var i = 0; i < stream.Length; i++)
                Matrix.Add(new CellProxy(this, i));
        }

        public void init()
        {
            _matrix = new ObservableCollection<MatrixCell>();

            foreach (var i in Enumerable.Range(0, 256))
            {
                tokens.Add((byte)i, i.ToString("X2"));
            } 
        }

        private void loadBytes(byte[] arr)
        {
            var rows = bytes.Length / 16;
            for (var i = 0; i < bytes.Length; i++)
            {
                var thevalue = tokens[bytes[i]];
                //Matrix.Add(new MatrixCell() { MyValue = thevalue });
                Matrix.Add(new CellProxy(this,i));
            } 
        }

        public byte ReadCellValue(int offset)
        {
            if(_stream != null)
            {
                byte[] arr = new byte[_stream.Length];
                _stream.Read(arr, offset, 1);
                return arr[offset];
            }
            return 10; 
        }

        public byte[] ReadCellValues(int offset, int length)
        {
            if(_stream != null)
            {
                byte[] arr = new byte[_stream.Length];
                _stream.Read(arr, offset, length);
                return arr;
            }
            return Enumerable.Repeat<byte>(10, length).ToArray();
        }

        public ObservableCollection<MatrixCell> Matrix
        {
            get
            {
                return _matrix;
            }
            set
            {
                _matrix = value;
                SetPropertyChanged(); 
            }
        }
    }
}
