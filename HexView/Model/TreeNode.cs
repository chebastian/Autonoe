using MVVMHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexView.Model
{
    public class TreeNode : ViewModelBase, ITreeNode
    {
        public TreeNode()
        {
            Children = new List<ITreeNode>();
        }


        private String name;
        private List<ITreeNode> children;
        public string Name { get => name; set { name = value; SetPropertyChanged(); } }
        public List<ITreeNode> Children
        {
            get => children; set { children = value; SetPropertyChanged(); }

        }
    }

        public interface ITreeNode
    {
        String Name { get; set; } 
        List<ITreeNode> Children { get; set; }
    }
}
