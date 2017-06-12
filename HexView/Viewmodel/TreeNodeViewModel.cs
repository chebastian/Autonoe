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
            _siblingViews = new ObservableCollection<TreeNodeViewModel>();
            //_siblings = node.Children; 
            Node = node; 
            SiblingViews = new ObservableCollection<TreeNodeViewModel>();


            _selectedListener = listener;
            History = new List<ITreeNode>();

            IsEmpty = node.Children.Any();
        } 
 
        public static TreeNodeViewModel CreateFileTreeRoot(ITreeNode node, IOnNodeSelected listener)
        {
            var tree =  new TreeNodeViewModel(node,listener);
            tree.NodeSelected = tree.SiblingViews.First();

            return tree;
        }

        private TreeNodeViewModel selectedNode; 
        public TreeNodeViewModel NodeSelected { get => selectedNode; set { selectedNode = value; DoClick(selectedNode);  SetPropertyChanged(); } }
        public List<ITreeNode> History;

        private ITreeNode node;
        public ITreeNode Node { get => node; set { node = value; SetSelectedNode(value); SetPropertyChanged(); } }

        public String RootName
        {
            get
            {
                return (Node as FileTreeNode).RootName;
            }
        }

        private void SetSelectedNode(ITreeNode selected)
        { 
            if (_siblings == null)
            {
                node = selected;
                return;
            } 

            node = selected;
            SiblingViews.Clear();
            //foreach (var child in Node.Children)
            //    ChildViews.Add(new TreeNodeViewModel(child,_selectedListener));
        }

        public ICommand Click
        {
            get => new MyCommandWrapper(
                //x => DoClick(x as TreeNodeViewModel),
                x => { },
                x => (x as TreeNodeViewModel).Node.HasChildren);
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

        public ObservableCollection<TreeNodeViewModel> SiblingViews
        {
            get
            {
                if(!_siblingViews.Any())
                {
                   // Task.Run(() => 
                    { 
                        foreach (var view in Node.Children)
                            _siblingViews.Add(new TreeNodeViewModel(view, this._selectedListener));

                        IsEmpty = _siblingViews.Any();
                    }

                    //foreach (var view in Node.Children)
                    //    _siblingViews.Add(new TreeNodeViewModel(view, this._selectedListener));

                    //IsEmpty = _siblingViews.Any(); 
                } 
                return _siblingViews;
            }
            set { _siblingViews = value;SetPropertyChanged(); }
        }

 
        private ObservableCollection<TreeNodeViewModel> _siblingViews;
        private List<ITreeNode> _siblings;
    }
}
