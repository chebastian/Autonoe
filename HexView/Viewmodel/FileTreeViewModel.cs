using HexView.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexView.Viewmodel
{
    class FileTreeViewModel : TreeNodeViewModel
    {
        public FileTreeViewModel()
        {

        }

        private List<FileTreeNode> _files;
        public List<FileTreeNode> Files
        {
            get
            {
                return Node.Children.Select(x => new FileTreeNode(x.Name,Node)).ToList();
            }

            set
            {
                _files = value;
                SetPropertyChanged();
            }

        }

    }
}
