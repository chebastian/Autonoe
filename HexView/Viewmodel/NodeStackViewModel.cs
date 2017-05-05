using MVVMHelpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HexView.Viewmodel
{
    public class NodeStackViewModel : ViewModelBase
    {
        private ObservableCollection<TreeNodeViewModel> trees; 
        public ObservableCollection<TreeNodeViewModel> Trees { get => trees; set { trees = value; SetPropertyChanged(); } }

        public NodeStackViewModel(List<TreeNodeViewModel> nodes)
        {
            Trees = new ObservableCollection<TreeNodeViewModel>();
            foreach (var node in nodes)
                Trees.Add(node);
        }
    }
}