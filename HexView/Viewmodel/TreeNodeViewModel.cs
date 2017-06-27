using HexView.Model;
using MVVMHelpers;
using MVVMHeplers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HexView.Viewmodel
{
    public class TreeNodeViewModel : ViewModelBase
    {
        public interface IOnNodeSelected
        {
            void onSelected(ITreeNode node,TreeNodeViewModel sender);
        }

        IOnNodeSelected _selectedListener;

        public TreeNodeViewModel()
        {

        }

        public TreeNodeViewModel(ITreeNode node, IOnNodeSelected listener)
        {
            _siblingViews = new ObservableCollection<TreeNodeViewModel>();
            //_siblings = node.Children; 
            Node = node; 
            SiblingViews = new ObservableCollection<TreeNodeViewModel>();
            IsLoaded = false;


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

        public void LoadNode(ITreeNode node)
        {
            IsLoaded = false;
            IsEmpty = node.Children.Any();
            SiblingViews.Clear();
            Node = node;
            foreach (var child in node.Children)
                SiblingViews.Add(new TreeNodeViewModel(child,_selectedListener));

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
                _selectedListener.onSelected(node.Node,this); 

        }

        public ObservableCollection<TreeNodeViewModel> SiblingViews
        {
            get
            {
                if(!_siblingViews.Any() && !IsLoaded)
                {
                   // Task.Run(() => 
                    {
                        //foreach (var view in Node.Children)
                        //    _siblingViews.Add(new TreeNodeViewModel(view, this._selectedListener));
                        //Task<List<TreeNodeViewModel>> task = GetChildrenAsync();
                        //var list = task;
                        loadChildren();

                        IsEmpty = _siblingViews.Any();
                    } 
                } 
                return _siblingViews;
            }
            set { _siblingViews = value;SetPropertyChanged(); }
        }


        private bool isLoading;
        public bool IsLoading { get => isLoading; set { isLoading = value; SetPropertyChanged(); } }

        public bool IsLoaded { get; private set; }

        public async void loadChildren()
        {
            isLoading = true;

            Task<List<TreeNodeViewModel>> task = GetChildrenAsync();
            var list = new List<TreeNodeViewModel>();
            list = await task;
            IsLoaded = true;

            isLoading = false; 
            SiblingViews = new ObservableCollection<TreeNodeViewModel>(list);
            IsEmpty = SiblingViews.Any();
        }

        private async Task<List<TreeNodeViewModel>> GetChildrenAsync()
        {
            var list = new List<TreeNodeViewModel>();
            foreach (var view in Node.Children)
                list.Add(new TreeNodeViewModel(view, this._selectedListener));

            return list;
        }


 
        private ObservableCollection<TreeNodeViewModel> _siblingViews;
        private List<ITreeNode> _siblings;
    }
}
