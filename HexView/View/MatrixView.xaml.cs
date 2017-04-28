using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// Interaction logic for MatrixView.xaml
    /// </summary>
    public partial class MatrixView : UserControl
    {
        public MatrixView()
        {
            InitializeComponent();

            var reader = File.OpenRead("./test.jpg");
            this.DataContext = new HexMatrixViewModel(reader);
            //var rows = 16;
            //var cols = 16;
            //for(var i = 0; i < rows; i++)
            //{
            //    var row = new RowDefinition();
            //    row.Height = GridLength.Auto;
            //    root.RowDefinitions.Add(new RowDefinition());
            //}

            //for(var i = 0; i < cols; i++)
            //{
            //    var col = new ColumnDefinition();
            //    col.Width = GridLength.Auto;
            //    root.ColumnDefinitions.Add(col); 
            //}
        }

        private void List_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void List_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(sender as ListBox); 
            var xx = pos.X;
            var x = (sender as ListBox).ActualWidth / 16;
            var y = (sender as ListBox).ActualHeight / 16;

            (DataContext as HexMatrixViewModel).Position = new Point((int)(pos.X / x), (int)(pos.Y / y));
        }
    }
}
