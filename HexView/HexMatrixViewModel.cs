using MVVMHelpers;
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
        private ObservableCollection<MatrixRow> _matrix;
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

            //for (var i = 0; i < stream.Length; i += 16)
            for (var i = 0; i < stream.Length/16; i++)
                Matrix.Add(new MatrixRow(this,i,16));

                //Matrix.Add(new CellProxy(this, i));
            //Matrix.Add(new MatrixRow())
        }

        public void init()
        {
            _matrix = new ObservableCollection<MatrixRow>();

            foreach (var i in Enumerable.Range(0, 256))
            {
                tokens.Add((byte)i, i.ToString("X2"));
            } 
        }

        private void loadBytes(byte[] arr)
        {
            var rows = bytes.Length / 16;

            for (var i = 0; i < bytes.Length; i+= 16)
            {
                var thevalue = tokens[bytes[i]];
                //Matrix.Add(new CellProxy(this,i));
            } 
        }

        public byte ReadCellValue(int offset)
        {
            if(_stream != null)
            {
                byte[] arr = new byte[_stream.Length];
                _stream.Seek(offset, SeekOrigin.Begin);
                return (byte)_stream.ReadByte();
                //return arr[offset];
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

        public ObservableCollection<MatrixRow> Matrix
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
