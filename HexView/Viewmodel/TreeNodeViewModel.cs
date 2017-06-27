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

        public TreeNodeViewModel(ITreeNode node, IOnNodeSelected listener,ITreeLoader loader)
        {
            _loader = loader;
            _siblingViews = new ObservableCollection<ITreeNode>();
            Node = node; 
            SiblingViews = new ObservableCollection<ITreeNode>();
            IsLoaded = false;


            _selectedListener = listener;
            History = new List<ITreeNode>();

            IsEmpty = node.Children.Any();
        } 

        private ITreeNode selectedNode; 
        public ITreeNode NodeSelected { get => selectedNode; set { selectedNode = value; DoClick(selectedNode);  SetPropertyChanged(); } }
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
                SiblingViews.Add(new FileTreeNode(child.Name,this.Node));
                //SiblingViews.Add(new FileTreeNode(child,_selectedListener));

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


        private void DoClick(ITreeNode node)
        {
            if (_selectedListener != null)
                _selectedListener.onSelected(node,this); 
        }

        public ObservableCollection<ITreeNode> SiblingViews
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
                        LoadSiblings();

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

        public async void LoadSiblings()
        {
            isLoading = true;

            Task<List<ITreeNode>> task = GetSiblingAsync();
            var list = new List<ITreeNode>();
            list = await task;
            IsLoaded = true;

            isLoading = false; 
            SiblingViews = new ObservableCollection<ITreeNode>(list);
            IsEmpty = SiblingViews.Any();
        }

        private async Task<List<ITreeNode>> GetSiblingAsync()
        {
            var list = _loader.LoadSiblings(Node);

            return list;
        }


 
        private ObservableCollection<ITreeNode> _siblingViews;
        private List<ITreeNode> _siblings;
        private ITreeLoader _loader;
    }
}
