using System.Collections.Generic;

namespace HexView
{
    public class UIEventaggregator<T>
    {
        public static UIEventaggregator<T> Instance = new UIEventaggregator<T>();

        List<IEventListener<T>> Listeners;

        public void Publish(T evt)
        {
            foreach (var listener in Listeners)
            {
                listener.Notify(evt);
            }
        }

        public void SubscribeTo(IEventListener<T> sub)
        {
            var present = Listeners.Contains(sub);
            if (!present)
                Listeners.Add(sub);
        }

        public void Unsubscribe(IEventListener<T> sub)
        {
            Listeners.Remove(sub);
        }

        private UIEventaggregator()
        {
            Listeners = new List<IEventListener<T>>();
        } 
    }

    public interface IEventListener<T>
    {
        void Notify(T evt);
    }
}