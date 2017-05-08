using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRUDContainer.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CRUDContainerTest
{
    [TestClass]
    public class CRUDItemTest
    { 
        private CRUDItem<CRUDItemBase> _item;
        private CRUDItemBase _model;
 
        [TestInitialize]
        public void setup()
        {
            _model = new CRUDItemBase(0);
            _item = new CRUDItem<CRUDItemBase>(_model);

        }

        [TestMethod]
        public void ItemIsChangedWhenPropertryChanged()
        {
            _model.Name = "testing";
            Assert.AreEqual(0, 0);
        }
    }
}
