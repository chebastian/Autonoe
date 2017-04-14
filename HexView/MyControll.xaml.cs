using MVVMHelpers;
using MyNamespace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HexView
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MyControll : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<MatrixCell> _matrix;

        public MyControll()
        {
            InitializeComponent();

            //var bytes = File.ReadAllBytes("./test.jpg");
            var reader = File.OpenRead("./test.jpg");
 
            //this.DataContext = new HexMatrixViewModel(bytes);
            this.DataContext = new HexMatrixViewModel(reader);
            //this.DataContext = this;
            //_matrix = new ObservableCollection<MatrixCell>();
            ////foreach (var x in Enumerable.Range(0, 100))
            ////    Matrix.Add(new MatrixCell() { MyValue = x.ToString() });

            //Dictionary<byte, String> tokens = new Dictionary<byte, string>();
            //foreach(var i in Enumerable.Range(0,256))
            //{
            //    tokens.Add((byte)i, i.ToString("X2"));
            //}

            //var rows = bytes.Length / 16;
            //for(var i = 0; i < bytes.Length; i++)
            //{
            //    var thevalue = tokens[bytes[i]];
            //    Matrix.Add(new MatrixCell() {MyValue= thevalue});
            //}
        } 

        public event PropertyChangedEventHandler PropertyChanged;
        public void SetPropertyChanged([CallerMemberName] String name = null)
        {
            if(PropertyChanged != null)
            {
                if (name != null)
                    PropertyChanged(this,new PropertyChangedEventArgs(name));
            }
        }
    }
}
