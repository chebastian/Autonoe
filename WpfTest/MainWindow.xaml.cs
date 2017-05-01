using HexView.Model;
using HexView.Viewmodel;
using System;
using System.Collections.Generic;
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

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //var node = new FileTreeNode(@"C:\totalcmd");
            var node = new FileTreeNode(@"C:\GOG Games");

            //var node = new TreeNode() { Name = "hest " };
            //node.Children.Add(new TreeNode() { Name = "Child" });

            //var second = new TreeNode() { Name = "Second Child" }; 
            //second.Children.Add(new TreeNode() { Name = "grandchild" });

            //node.Children.Add(second);

            Tree.DataContext = new TreeNodeViewModel(node);
        }
    }
}
