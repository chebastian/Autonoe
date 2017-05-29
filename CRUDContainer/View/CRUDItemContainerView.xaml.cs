using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRUDContainer.View
{
    /// <summary>
    /// Interaction logic for CRUDItemContainerView.xaml
    /// </summary>
    public partial class CRUDItemContainerView : UserControl
    {
        public CRUDItemContainerView()
        {
            InitializeComponent();
            layoutRoot.DataContext = this;
        }

        public object TemplateControl
        {
            get { return (object)GetValue(TemplateControlProperty); }
            set { SetValue(TemplateControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TemplateControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemplateControlProperty =
            DependencyProperty.Register("TemplateControl", typeof(object), typeof(CRUDItemContainerView), new PropertyMetadata(0));

        public ICommand RemoveCommand
        {
            get { return (ICommand)GetValue(RemoveCommandProperty); }
            set { SetValue(RemoveCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TemplateControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RemoveCommandProperty =
            DependencyProperty.Register("RemoveCommand", typeof(ICommand), typeof(CRUDItemContainerView), new PropertyMetadata(null));
    }
}
