using HexView.Model;
using MVVMHelpers;

namespace HexView.Viewmodel
{
    public class TreeNodeStack : ViewModelBase
    {
        private ITreeNode _parent;
        private IEventListener<ITreeNode> _children;

        public TreeNodeStack(ITreeNode parent, IEventListener<ITreeNode> children)
        {
            Parent = parent;
            _children = children;
        }

        public ITreeNode Parent { get => _parent; set => _parent = value; }
    }
}