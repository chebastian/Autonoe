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
        public interface IOnNodeSelected
        {
            void onSelected(ITreeNode node);
        }

        IOnNodeSelected _selectedListener;

        public TreeNodeViewModel(ITreeNode node, IOnNodeSelected listener)
        {
            Node = node;
            ChildViews = new ObservableCollection<TreeNodeViewModel>();

            _selectedListener = listener;
            History = new List<ITreeNode>(); 

            foreach (var child in Node.Children)
                ChildViews.Add(new TreeNodeViewModel(child,listener));
        }

        private TreeNodeViewModel selectedNode; 
        public TreeNodeViewModel NodeSelected { get => selectedNode; set { selectedNode = value; SetPropertyChanged(); } }
        public List<ITreeNode> History;

        private ITreeNode node;
        public ITreeNode Node { get => node; set { SetSelectedNode(value); SetPropertyChanged(); } }

        private void SetSelectedNode(ITreeNode selected)
        { 
            if (ChildViews == null)
            {
                node = selected;
                return;
            }


            node = selected;
            ChildViews.Clear();
            foreach (var child in Node.Children)
                ChildViews.Add(new TreeNodeViewModel(child,_selectedListener));
        }

        public ICommand Click
        {
            get => new MyCommandWrapper(x => DoClick(x as TreeNodeViewModel), (x) => (x as TreeNodeViewModel).ChildViews.Any());
        }

        public ICommand GoBack
        {
            get => new MyCommandWrapper(x => DoGoBack(), () => History.Any());
        }

        private void DoGoBack()
        {
            var last = History[History.Count - 1];
            History.Remove(last);
            SetSelectedNode(last);
        }


        private void DoClick(TreeNodeViewModel node)
        {
            //node.Node.Name = "this is me";
            //History.Add(Node);
            //Node = node.Node;
            if (_selectedListener != null)
                _selectedListener.onSelected(node.Node);

        }

        public ObservableCollection<TreeNodeViewModel> ChildViews
        {
            get => childViews;
            set { childViews = value;SetPropertyChanged(); }
        }
 
        private ObservableCollection<TreeNodeViewModel> childViews; 
    }
}
