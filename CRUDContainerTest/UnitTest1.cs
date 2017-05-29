using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRUDContainer.ViewModel;
using Prism.Events;
using System.Collections.Generic;
using CRUDContainer.Model;
using System.Linq;

namespace CRUDContainerTest
{ 

    public class CIVMBase
    {
        protected CRUDItemListViewModel vm;
        protected List<CRUDItemViewModel<CRUDItemBase>> _original;

        public void init()
        {
            IEventAggregator agg = new EventAggregator(); 
            _original = new List<CRUDItemViewModel<CRUDItemBase>>();
            _original.Add(new CRUDItemViewModel<CRUDItemBase>(new CRUDItem<CRUDItemBase>(new CRUDItemBase(5))));
            _original.Add(new CRUDItemViewModel<CRUDItemBase>(new CRUDItem<CRUDItemBase>(new CRUDItemBase(5))));
            _original.Add(new CRUDItemViewModel<CRUDItemBase>(new CRUDItem<CRUDItemBase>(new CRUDItemBase(5)))); 

            vm = new CRUDItemListViewModel(agg,_original); 
        }
    }

    [TestClass]
    public class CIVM
    { 

        [TestClass] 
        public class Commands : CIVMBase
        {
            [TestInitialize]
            public void setup()
            {
                init();
            }

            [TestMethod]
            public void CanAddItemsToCollection()
            {
                var count = vm.VisibleCollection.Count;
                vm.NewItemCommand.Execute(null);

                Assert.IsTrue(count < vm.VisibleCollection.Count);
            } 
        }

        [TestClass] 
        [TestCategory("SomeName")]
        public class RemovedItems : CIVMBase
        {
            [TestInitialize]
            public void setup()
            {
                init();
            }

            [TestMethod]
            public void ShouldBeRemoved()
            {
                var count = vm.VisibleCollection.Count;
                vm.NewItemCommand.Execute(new CRUDItemViewModel<CRUDItemBase>(new CRUDItem<CRUDItemBase>(new CRUDItemBase(5)))); 
                vm.RemoveCommand.Execute(vm.VisibleCollection[0]);

                Assert.IsTrue(count == vm.VisibleCollection.Count); 
            }

            [TestMethod]
            public void OriginalRemovedItemsAreRemembered()
            { 
                vm.NewItemCommand.Execute(new CRUDItemViewModel<CRUDItemBase>(new CRUDItem<CRUDItemBase>(new CRUDItemBase(5)))); 
                vm.RemoveCommand.Execute(vm.OriginalCollection[0]);
                vm.RemovedItems.Contains(0);
            } 
        } 


        [TestClass]
        public class CreatedItems : CIVMBase
        {

            [TestInitialize]
            public void setup()
            {
                init();
            }

            [TestMethod]
            public void NewlyCreatedItemsAreRemembered()
            { 
                 var theItem = new CRUDItemViewModel<CRUDItemBase>(new CRUDItem<CRUDItemBase>(new CRUDItemBase(5)));
                vm.NewItemCommand.Execute(theItem); 

                Assert.IsTrue(vm.CreatedItems.Contains(theItem.MyId), "Created item not remembered ");
            } 

            [TestMethod]
            public void ShouldNotBeRememberedOnRemove()
            {
                 var theItem = new CRUDItemViewModel<CRUDItemBase>(new CRUDItem<CRUDItemBase>(new CRUDItemBase(5)));
                vm.NewItemCommand.Execute(theItem);
                vm.RemoveCommand.Execute(theItem);

                Assert.IsFalse(vm.RemovedItems.Contains(theItem.MyId),"Only original items should be remembered for deletion"); 
                Assert.IsFalse(vm.CreatedItems.Contains(theItem.MyId),"Removed items should not be remembeed for creation");
            }
        }

        [TestClass]
        public class ItemChange : CIVMBase
        {

            [TestInitialize]
            public void setup()
            {
                init();
            }

            [TestMethod]
            public void ShouldFlagItemToUpdate()
            {
                vm.VisibleCollection[0].SomeParam = "testing";
                var hasItem = vm.UpdatedItems.Contains(vm.VisibleCollection[0].MyId);
                Assert.IsTrue(hasItem, "No update forced");
            }
        }
    }

}
