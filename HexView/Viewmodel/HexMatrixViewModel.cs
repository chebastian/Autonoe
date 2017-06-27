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
using System.Threading;

namespace HexView
{
    public class HexMatrixViewModel : ViewModelBase, ICellProvider, IEventListener<CellSelectedEvent>
    {
        private ObservableCollection<MatrixRow> _matrix;
        private Dictionary<byte, String> tokens = new Dictionary<byte, string>();
        private byte[] bytes;
        private FileStream _stream;
        private ObservableCollection<MatrixCell> _data;

        public ObservableField<int> LoadedRows { get; set; } = new ObservableField<int>(20);
        public ObservableField<int> RowsToLoad { get; set; } = new ObservableField<int>(100);

        public int Width => 16;
        public int Height => 16;

        public HexMatrixViewModel()
        {
            init();
            //loadBytes(new byte[] { 255, 255, 123 });
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

            for (var i = 0; i < stream.Length/Width; i++)
            {
                Matrix.Add(new MatrixRow(this,i,Width));
            } 

            FileSize = String.Format("File Size: {0}", Matrix.Count * Width);
        }

        public HexMatrixViewModel(List<MatrixRow> rows)
        {
            MyData = new ObservableCollection<MatrixCell>();
            init();

            foreach (var row in rows)
                Matrix.Add(row); 

            FileSize = String.Format("File Size: {0}", Matrix.Count * Width);
        }

        public void Clear()
        {
            Matrix.Clear();
        }

        public List<MatrixRow> loadStream(FileStream stream)
        {
            if (_stream != null)
                _stream.Close();

            RowsToLoad.Set( (int) (stream.Length / Width) );
            LoadedRows.Set(0);
            _stream = stream;
            var res = new List<MatrixRow>();
            for (var i = 0; i < stream.Length/Width; i++)
            {
                res.Add(new MatrixRow(this,i,Width));
                LoadedRows.Set(i);
            }

            FileSize = String.Format("File Size: {0}", res.Count * Width); 

            return res;
        } 

        public async void loadStreamAsync(FileStream stream,CancellationTokenSource token)
        {

            Matrix.Clear();
            if( _cancelLoading != null )
            {
                _cancelLoading.Cancel();
            }

            _cancelLoading = token;

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            await Task.Factory.StartNew<List<MatrixRow>>(() =>
            {
                if (_stream != null)
                    _stream.Close();

                RowsToLoad.Set( (int) (stream.Length / Width) );
                LoadedRows.Set(0);
                _stream = stream;
                var res = new List<MatrixRow>();
                var length = stream.Length / Width;
                for (var i = 0; i < length; i++)
                {
                    token.Token.ThrowIfCancellationRequested();
                    res.Add(new MatrixRow(this,i,Width));
                    LoadedRows.Set(i);

                }

                FileSize = String.Format("File Size: {0}", res.Count * Width); 

                return res;

            },token.Token).ContinueWith(x =>
            {
                try
                { 
                    Matrix = new ObservableCollection<MatrixRow>(x.Result);
                }
                catch(AggregateException e)
                {
                    var mess = e.Message;
                }

            },scheduler);
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
            var rows = bytes.Length / Width;

            for (var i = 0; i < bytes.Length; i+= Width)
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
            else
            {
                throw new Exception("Stream null, cannot read file");
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
            get => _fileSize;

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
        private CancellationTokenSource _cancelLoading;

        public Point Position { get => _pos; set { _pos = value; SetPropertyChanged(); } }
    }
}
