using System;
using System.Collections.Generic;

namespace HexView
{
    public class UIEventaggregator<T>
    {
        public static UIEventaggregator<T> Instance = new UIEventaggregator<T>();

        List<Action<T>> Actions;
        List<IEventListener<T>> Listeners;

        private UIEventaggregator()
        {
            Listeners = new List<IEventListener<T>>();
            Actions = new List<Action<T>>();
        } 

        public void Publish(T evt)
        {
            foreach (var listener in Listeners)
            {
                listener.Notify(evt);
            }

            foreach (var action in Actions)
                action(evt);
        }

        public void SubscribeTo(IEventListener<T> sub)
        {
            var present = Listeners.Contains(sub);
            if (!present)
                Listeners.Add(sub);
        }

        public void Subscribe(Action<T> action)
        {
            Actions.Add(action);
        }

        public void Unsubscribe(IEventListener<T> sub)
        {
            Listeners.Remove(sub);
        } 
    }

    public interface IEventListener<T>
    {
        void Notify(T evt);
    } 
}