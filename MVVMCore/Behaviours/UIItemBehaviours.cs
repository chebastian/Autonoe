using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVMCore.Events
{
    public class EventCommandHandler
    {
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(EventCommandHandler), new UIPropertyMetadata(null));

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(EventCommandHandler), new UIPropertyMetadata(null));

        public static readonly DependencyProperty RoutedEventProperty =
            DependencyProperty.RegisterAttached("RoutedEvent", typeof(RoutedEvent), typeof(EventCommandHandler), new UIPropertyMetadata(null, RoutedEventChanged));

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand) obj.GetValue(CommandProperty);
        }

        public static object GetCommandParameter(DependencyObject obj)
        {
            return obj.GetValue(CommandParameterProperty);
        }

        public static RoutedEvent GetRoutedEvent(DependencyObject obj)
        {
            return (RoutedEvent) obj.GetValue(RoutedEventProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }

        public static void SetRoutedEvent(DependencyObject obj, RoutedEvent value)
        {
            obj.SetValue(RoutedEventProperty, value);
        }

        private static void ExecuteCommand(object sender, RoutedEventArgs args)
        {
            var commandSource = sender as DependencyObject;

            if (commandSource == null)
            {
                return;
            }

            var command = commandSource.GetValue(CommandProperty) as ICommand;
            var commandParameter = commandSource.GetValue(CommandParameterProperty);

            if (command == null)
            {
                return;
            }

            if (command.CanExecute(commandParameter))
            {
                command.Execute(commandParameter);
            }
        }

        private static void RoutedEventChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var inputElement = sender as IInputElement;

            var oldRoutedEvent = args.OldValue as RoutedEvent;
            var newRoutedEvent = args.NewValue as RoutedEvent;

            if (inputElement != null)
            {
                inputElement.AddHandler(newRoutedEvent, new RoutedEventHandler(ExecuteCommand));
            }
        }
    }

    public    static class UIItemBehaviours
    {
        public static readonly DependencyProperty LClickCommandProperty =
            DependencyProperty.RegisterAttached(
                "LClickCommand",
                typeof(ICommand),
                typeof(UIItemBehaviours),
                new PropertyMetadata( null, new PropertyChangedCallback(LClickCommandProperty_PropChanged)) 
                ); 

        public static readonly DependencyProperty CommandParam =
            DependencyProperty.RegisterAttached(
                "CommandParam",
                typeof(object),
                typeof(UIItemBehaviours),
                new PropertyMetadata(null) 
                );

        public static void SetLClickCommand(UIElement element, ICommand command)
        {
            element.SetValue(LClickCommandProperty, command);
        }

        public static ICommand GetLClickCommand(UIElement element)
        {
            return (ICommand)element.GetValue(LClickCommandProperty);
        }

        private static void LClickCommandProperty_PropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = (d as UIElement);
            element.RemoveHandler(UIElement.MouseLeftButtonDownEvent,new RoutedEventHandler(LeftClickHandler));
            element.AddHandler(UIElement.MouseLeftButtonDownEvent,new RoutedEventHandler(LeftClickHandler));
        } 

        private static void LeftClickHandler(object sender, RoutedEventArgs e)
        {
            UIElement element = (sender as UIElement);
            ICommand command = GetLClickCommand(element);
            if(command.CanExecute(e))
            {
                command.Execute(sender); 
            } 
        }
    }
}
