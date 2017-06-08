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
            _childViews = new ObservableCollection<TreeNodeViewModel>();
            _children = node.Children;
            ChildViews = new ObservableCollection<TreeNodeViewModel>();

            Node = node; 

            _selectedListener = listener;
            History = new List<ITreeNode>();


            //foreach (var child in Node.Children)
            //    ChildViews.Add(new TreeNodeViewModel(child,listener)); 
            //IsEmpty = ChildViews.Any();

            IsEmpty = _children.Any();
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
            //foreach (var child in Node.Children)
            //    ChildViews.Add(new TreeNodeViewModel(child,_selectedListener));
        }

        public ICommand Click
        {
            get => new MyCommandWrapper(x => DoClick(x as TreeNodeViewModel), (x) => (x as TreeNodeViewModel).Node.HasChildren);
        }

        bool _empty;
        public bool IsEmpty
        {
            get => _empty;
            set
            {
                _empty = value;
                SetPropertyChanged();
            }
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
            if (_selectedListener != null)
                _selectedListener.onSelected(node.Node);

        }

        public ObservableCollection<TreeNodeViewModel> ChildViews
        {
            get
            {
                if(!_childViews.Any())
                {
                    foreach (var view in _children)
                        _childViews.Add(new TreeNodeViewModel(view, this._selectedListener));

                    IsEmpty = _childViews.Any(); 
                } 
                return _childViews;
            }
            set { _childViews = value;SetPropertyChanged(); }
        }
 
        private ObservableCollection<TreeNodeViewModel> _childViews;
        private List<ITreeNode> _children;
    }
}
