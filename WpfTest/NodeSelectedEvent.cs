using HexView.Model;
using Prism.Events;

namespace WpfTest
{
    public class NodeSelectedEvent : MyEvents<NodeSelectedEventArg> 
    {
    }

    public class MyEvents<T> : PubSubEvent<T>
    {
        public override void Publish(T payload)
        {
            base.Publish(payload);
        }
    }

    public class NodeSelectedEventArg
    {
        public NodeSelectedEventArg(ITreeNode node)
        {
            Node = node;
        }

        public ITreeNode Node { get; private set; } 
    }
}