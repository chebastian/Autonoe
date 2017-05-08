using HexView.Model;
using HexView.Viewmodel;
using MVVMHelpers;
using MVVMHeplers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, TreeNodeViewModel.IOnNodeSelected,INotifyPropertyChanged
    {
        ObservableCollection<TreeNodeViewModel> _fileTree;
        public List<NodeStackViewModel> _test { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            //var node = new FileTreeNode(@"C:\totalcmd");
            //var node = new FileTreeNode(@"C:\GOG Games");
            var node = new FileTreeNode(@"C:\testfolder",null);

            //var node = new TreeNode() { Name = "hest " };
            //node.Children.Add(new TreeNode() { Name = "Child" });

            //var second = new TreeNode() { Name = "Second Child" }; 
            //second.Children.Add(new TreeNode() { Name = "grandchild" });

            //node.Children.Add(second);

            this.DataContext = this;
            Tree.DataContext = new TreeNodeViewModel(node, this);
            FileTree = new ObservableCollection<TreeNodeViewModel>();
            _test = new List<NodeStackViewModel>();

            var l = new List<TreeNodeViewModel>();
            //_test.Add(new NodeStackViewModel(FileTree));
        }

        public ObservableCollection<TreeNodeViewModel> FileTree
        {
            get => _fileTree;
            set
            {
                _fileTree = value;
                SetPropertyChanged();
            }
        }

        public void onSelected(ITreeNode node)
        {
            FileTree.Add(new TreeNodeViewModel(node,this)); 
            //if(node.Parent != null)
            //{
            //    if(!_test.ContainsKey(node.Parent))
            //    {
            //        _test.Add(node.Parent, new List<TreeNodeViewModel>());
            //    }

            //    _test[node.Parent].Add(new TreeNodeViewModel(node,this));
            //}
            //else
            //{
            //    var first = new FileTreeNode();
            //    if(!_test.ContainsKey(node))
            //    {
            //        _test.Add(first,new List<TreeNodeViewModel>());
            //    }

            //    _test[first].Add(new TreeNodeViewModel(node,this));
            //}
        }

        public ICommand OnClose
        {
            get
            {
                return new MyCommandWrapper(x => {
                    FileTree.Remove((x as TreeNodeViewModel));
                });
            }
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
