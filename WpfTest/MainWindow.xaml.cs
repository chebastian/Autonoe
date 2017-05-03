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

        public MainWindow()
        {
            InitializeComponent();

            //var node = new FileTreeNode(@"C:\totalcmd");
            //var node = new FileTreeNode(@"C:\GOG Games");
            var node = new FileTreeNode(@"C:\testfolder");

            //var node = new TreeNode() { Name = "hest " };
            //node.Children.Add(new TreeNode() { Name = "Child" });

            //var second = new TreeNode() { Name = "Second Child" }; 
            //second.Children.Add(new TreeNode() { Name = "grandchild" });

            //node.Children.Add(second);

            this.DataContext = this;
            Tree.DataContext = new TreeNodeViewModel(node, this);
            FileTree = new ObservableCollection<TreeNodeViewModel>();
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
