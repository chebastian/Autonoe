using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HexView;

namespace UnitTestsLan
{
    [TestClass]
    public class EventAggregatorTest
    {
        class MockEventA  { };
        class MockEventB  { };

        class MockListenerBase<T> : IEventListener<T>,IDisposable
        { 
            public bool iHasBeenCalled = false;
            public int CallTimes = 0;

            public void Dispose()
            {
                UIEventaggregator<T>.Instance.Unsubscribe(this);
            }

            public void Notify(T evt)
            {
                iHasBeenCalled = true;
                CallTimes++;
            }
        }

        class MockListener : MockListenerBase<MockEventA>
        {
        }

        class MockListenerB : MockListenerBase<MockEventB>
        {
        }

        [TestInitialize]
        public void setup()
        {
        }

        [TestMethod]
        public void CanSubscribeToEvents()
        {
            //EvtMgr<TestEvent>.Instance.SubscribeTo<TestEvent>(new MockListener());
            using(var listener = new MockListener())
            {
                UIEventaggregator<MockEventA>.Instance.SubscribeTo(listener);
                UIEventaggregator<MockEventA>.Instance.Publish(new MockEventA());

                Assert.IsTrue(listener.iHasBeenCalled, "Listener has not been nofied"); 
            }
        }

        [TestMethod]
        public void CanHoldMoreEvents()
        {
            //EvtMgr<TestEvent>.Instance.SubscribeTo<TestEvent>(new MockListener());
            var listener_a = new MockListener();
            var listener_b = new MockListenerB();
            UIEventaggregator<MockEventA>.Instance.SubscribeTo(listener_a);
            UIEventaggregator<MockEventA>.Instance.Publish(new MockEventA()); 

            UIEventaggregator<MockEventB>.Instance.SubscribeTo(listener_b);

            Assert.IsTrue(listener_a.iHasBeenCalled, "Listener has not been nofied"); 
            Assert.IsFalse(listener_b.iHasBeenCalled, "Unpublished listener has been notified"); 
        }

        [TestMethod]
        public void LisenerCanUnsubscribe()
        {
            var listener_a = new MockListener();
            var secondListener = new MockListener();

            UIEventaggregator<MockEventA>.Instance.SubscribeTo(listener_a);
            UIEventaggregator<MockEventA>.Instance.SubscribeTo(secondListener);

            UIEventaggregator<MockEventA>.Instance.Unsubscribe(listener_a);
            UIEventaggregator<MockEventA>.Instance.Publish(new MockEventA()); 

            Assert.IsFalse(listener_a.iHasBeenCalled, "Listener has not been nofied"); 
            Assert.IsTrue(secondListener.iHasBeenCalled, "Listener has not been nofied"); 
        }

        [TestMethod]
        public void ListenersCanOnlySubscribeOnce()
        {
            var listener_a = new MockListener();
            var secondListener = new MockListener();
 
            UIEventaggregator<MockEventA>.Instance.SubscribeTo(listener_a);
            UIEventaggregator<MockEventA>.Instance.SubscribeTo(listener_a); 
            UIEventaggregator<MockEventA>.Instance.Publish(new MockEventA()); 
 
            Assert.AreEqual(listener_a.CallTimes,1, "Listener has not been nofied"); 
        }

        [TestMethod]
        public void CanListenWithAction()
        {
            int closureObj = 0;
            UIEventaggregator<MockListener>.Instance.Subscribe((x) =>
            {
                closureObj++;
            });


            UIEventaggregator<MockListener>.Instance.Publish(new MockListener());

            Assert.AreEqual(1, closureObj, "Not called"); 
        }
    }
}
