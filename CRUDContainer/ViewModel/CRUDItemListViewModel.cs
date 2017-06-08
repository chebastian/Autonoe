using CRUDContainer.Model;
using CRUDContainer.ViewModel;
using MVVMHelpers;
using MVVMHeplers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static MVVMCore.Events.TestEvents;

namespace CRUDContainer.ViewModel
{
    public class CRUDItemListViewModel<T> : ViewModelBase where T : IEquatable<T>, INotifyPropertyChanged
    { 
        public CRUDItemListViewModel(IEventAggregator aggregator, List<CRUDItemViewModel<CRUDItemBase>> original)
        { 
            init();

            OriginalCollection = new ObservableCollection<CRUDItemViewModel<CRUDItemBase>>();
            OriginalCollection.AddRange(original);


            VisibleCollection = new ObservableCollection<CRUDItemViewModel<CRUDItemBase>>();
            VisibleCollection.AddRange(OriginalCollection);

            foreach(var item in VisibleCollection)
                item.PropertyChanged += ItemDidChange;
        }

        private void ItemDidChange(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
            var item = (sender as CRUDItemViewModel<CRUDItemBase>);
            UpdatedItems.Add(item.MyId);
        }

        private void init()
        {
            Number = 0;

            TemplateItem = new CRUDItemViewModel<CRUDItemBase>(new CRUDItem<CRUDItemBase>(new CRUDItemBase(5)));
            CreatedItems = new ObservableCollection<int>();
            RemovedItems = new ObservableCollection<int>();
            UpdatedItems = new ObservableCollection<int>(); 

            RemoveCommand = new MyCommandWrapper(
                (x) => 
                {
                    var theItem = (x as CRUDItemViewModel<CRUDItemBase>);
                    Console.WriteLine("Test" + (x as CRUDItemViewModel<CRUDItemBase> ).MyId);
                    VisibleCollection.Remove(x as CRUDItemViewModel<CRUDItemBase>);

                    if(OriginalCollection.Contains(x))
                        RemovedItems.Add(theItem.MyId);


                    VisibleCollection.Remove(theItem);
                    CreatedItems.Remove(theItem.MyId);

                    SetPropertyChanged("Diff");
                } );

        }

        int number;
        public int Number { get => number++; set { number = value; SetPropertyChanged(); } }

        private ObservableCollection<CRUDItemViewModel<CRUDItemBase>> _originalCollection; 
        public ObservableCollection<CRUDItemViewModel<CRUDItemBase>> OriginalCollection
        {
            get => _originalCollection;
            set
            {
                _originalCollection = value;
                SetPropertyChanged();
            }
        } 

        private ObservableCollection<CRUDItemViewModel<CRUDItemBase>> _visibleCollection; 
        public ObservableCollection<CRUDItemViewModel<CRUDItemBase>> VisibleCollection
        {
            get => _visibleCollection;
            set
            {
                _visibleCollection = value;
                SetPropertyChanged();
            }
        } 

        private ObservableCollection<String> _diff; 
        public ObservableCollection<String> Diff
        {
            get
            {
                //var same = VisibleCollection.Where(x => OriginalCollection.Contains(x)).ToList();
                //var diff = VisibleCollection.Except(same).ToList();
                //var removed = VisibleCollection.Except(same).ToList();
                //var added = VisibleCollection.Where(x => !OriginalCollection.Contains(x)).ToList(); 

                return _diff;
            }
            set
            {
                _diff = value;
                SetPropertyChanged();
            }
        } 
 
        public ICommand NewItemCommand
        {
            get
            {
                return new MyCommandWrapper((x) =>
                {
                    VisibleCollection.Add(x as CRUDItemViewModel<CRUDItemBase>);
                    CreatedItems.Add(VisibleCollection.Last().MyId);
                    SetPropertyChanged("Diff");

                    TemplateItem = new CRUDItemViewModel<CRUDItemBase>(new CRUDItem<CRUDItemBase>(new CRUDItemBase(5)));
                });
            }
        } 

        [Import(typeof(IEventAggregator))]
        private ICommand removeCommand;

        private IEventAggregator _aggregator;
        public ObservableCollection<T> Stuff;

        private ObservableCollection<int> _removed;
        private ObservableCollection<int> _created;
        private ObservableCollection<int> _updated;
        private CRUDItemViewModel<CRUDItemBase> _template;
        private CRUDItemViewModel<T> _t_Template;

        public ICommand RemoveCommand
        {
            get => removeCommand;
            set
            {
                removeCommand = value;
                SetPropertyChanged();
            }
        }

        public CRUDItemViewModel<CRUDItemBase> TemplateItem { get => _template; set { _template = value; SetPropertyChanged(); } }

        public ObservableCollection<int> RemovedItems { get => _removed; set { _removed = value; SetPropertyChanged(); } }
        public ObservableCollection<int> CreatedItems { get => _created; set { _created = value; SetPropertyChanged(); } }
        public ObservableCollection<int> UpdatedItems { get => _updated; set { _updated = value; SetPropertyChanged(); } } 
    }

    public class CRUDItemBaseVM : CRUDItemViewModel<CRUDItemBase>
    {

    }
}
