using MVVMHelpers;
using MVVMHeplers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Prism.Events;

namespace HexView
{
    public class HexMatrixViewModel : ViewModelBase, ICellProvider, IEventListener<CellSelectedEvent>
    {
        private ObservableCollection<MatrixRow> _matrix;
        private Dictionary<byte, String> tokens = new Dictionary<byte, string>();
        private byte[] bytes;
        private FileStream _stream;
        private ObservableCollection<MatrixCell> _data;

        public int Width => 16;
        public int Height => 16;

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

            MyData = new ObservableCollection<MatrixCell>();
            //for (var i = 0; i < stream.Length; i += 16)
            for (var i = 0; i < stream.Length/16; i++)
            {
                Matrix.Add(new MatrixRow(this,i,16));
                //MyData.Add(new CellProxy(this,i));
            }

            for (var i = 0; i < stream.Length; i++)
            {
                MyData.Add(new CellProxy(this, i));
            }


                //Matrix.Add(new CellProxy(this, i));
            //Matrix.Add(new MatrixRow())
        }

        public void init()
        {
            UIEventaggregator<CellSelectedEvent>.Instance.SubscribeTo(this);
            _matrix = new ObservableCollection<MatrixRow>();

            foreach (var i in Enumerable.Range(0, 256))
            {
                tokens.Add((byte)i, i.ToString("X2"));
            }

            FileSize = "FFF";
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

        public void Notify(CellSelectedEvent evt)
        {
            //evt.matrixCell.MyValue.Value = "DD";
        }

        String _fileSize;
        public String FileSize
        {
            get => "This is my string";

            set
            {
                _fileSize = value;
                SetPropertyChanged();
            }
        }

        public ObservableCollection<MatrixCell> MyData
        {
            get => _data;
            set
            { 
                _data = value;
                SetPropertyChanged();
            }
        }

        public ICommand MouseMoveCommand
        {
            get => new MyCommandWrapper(x =>
            {
            });
        }

        public ICommand MouseDownCommand
        {
            get => new MyCommandWrapper(x =>
            {
                foreach (var data in MyData)
                    if (data.Marked)
                        data.Marked = false;

                _isHighlighting = true;
            });
        }

        public void Update()
        {
            SetPropertyChanged("Matrix");
        }

        public ICommand MouseUpCommand
        {
            get => new MyCommandWrapper(x =>
            {
                _isHighlighting = false;
            });
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

        Point _pos;
        private bool _isHighlighting;

        public Point Position { get => _pos; set { _pos = value; SetPropertyChanged(); } }
    }
}
