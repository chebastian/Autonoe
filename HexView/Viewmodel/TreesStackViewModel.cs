using MVVMHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexView.Viewmodel
{
    public class TreesStackViewModel : ViewModelBase
    {
        public ObservableCollection<TreeNodeViewModel> Views
        {
            get;set;
        }
    }
}
