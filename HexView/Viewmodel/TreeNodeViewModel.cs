using HexView.Model;
using MVVMHelpers;
using MVVMHeplers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HexView.Viewmodel
{
    public class TreeNodeViewModel : ViewModelBase
    {
        public TreeNodeViewModel(ITreeNode node)
        {
            Node = node;
            ChildViews = new ObservableCollection<TreeNodeViewModel>();



            foreach (var child in Node.Children)
                ChildViews.Add(new TreeNodeViewModel(child));
        }

        private TreeNodeViewModel selectedNode; 
        public TreeNodeViewModel NodeSelected { get => selectedNode; set { selectedNode = value; SetPropertyChanged(); } }

        private ITreeNode node;
        public ITreeNode Node { get => node; set { node = value; SetSelectedNode(node); SetPropertyChanged(); } }

        private void SetSelectedNode(ITreeNode node )
        {
            if (ChildViews == null)
                return;

            ChildViews.Clear();
            foreach (var child in Node.Children)
                ChildViews.Add(new TreeNodeViewModel(child));
        }

        public ICommand Click
        {
            get => new MyCommandWrapper(x => DoClick(x as TreeNodeViewModel));
        }

        private void DoClick(TreeNodeViewModel node)
        {
            node.Node.Name = "this is me";
            Node = node.Node;
        }

        public ObservableCollection<TreeNodeViewModel> ChildViews
        {
            get => childViews;
            set { childViews = value;SetPropertyChanged(); }
        }


        private ObservableCollection<TreeNodeViewModel> childViews; 
    }
}
