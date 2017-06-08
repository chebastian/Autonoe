using MVVMHelpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
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
            get => children;
            set {
                children = value;
                HasChildren = children.Count > 0;
                SetPropertyChanged();
            }

        }

        private bool hasChildren; 
        public bool HasChildren
        {
            get => hasChildren;
            set
            {
                hasChildren = value;
                SetPropertyChanged();
            }
        }

        public ITreeNode Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class FileTreeNode : ViewModelBase,ITreeNode
    {
        string rootDirectory;
        public FileTreeNode()
        {

        }
 
        public FileTreeNode(String root, ITreeNode parent)
        {
            rootDirectory = root;
            Name = rootDirectory;
            Parent = parent;
            children = null; 
        }

        //public FileTreeNode(String root)
        //    :base()
        //{
        //    rootDirectory = root;
        //    children = null;
        //    Name = rootDirectory;
        //}

        private String name;
        private List<ITreeNode> children;

        public string Name { get => name; set { name = value; SetPropertyChanged(); } }
        public List<ITreeNode> Children
        {
            get
            {
                if(children == null)
                {
                    children = new List<ITreeNode>();
                    if (Directory.Exists(rootDirectory))
                    {
                        children.AddRange( Directory.GetDirectories(rootDirectory).Select(x => new FileTreeNode(x,this)) ); 
                        children.AddRange( Directory.GetFiles(rootDirectory).Select(x => new FileTreeNode(x,this)) ); 
                    }
                }

                HasChildren = Directory.Exists(rootDirectory);
                //HasChildren = children.Count > 0;
                return children;
            }

            set { children = value; SetPropertyChanged(); }

        }

        private bool hasChildren; 
        public bool HasChildren
        {
            get => hasChildren;
            set
            {
                hasChildren = value;
                SetPropertyChanged();
            }
        }

        private ITreeNode parent;
        public ITreeNode Parent { get => parent; set { parent = value; SetPropertyChanged(); } }
    }

        public interface ITreeNode
    {
        ITreeNode Parent { get; set; }
        String Name { get; set; } 
        List<ITreeNode> Children { get; set; }
        bool HasChildren { get; set; }
    }
}
