using MVVMHeplers;
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

namespace HexView.View
{
    /// <summary>
    /// Interaction logic for CloseableContainer.xaml
    /// </summary>
    public partial class CloseableContainer : UserControl
    {
        public CloseableContainer()
        {
            InitializeComponent();
            this.layoutRoot.DataContext = this;
        } 

        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(CloseableContainer), new PropertyMetadata(new MyCommandWrapper((x) => {})));



        public object TemplateControl
        {
            get { return (object)GetValue(TemplateControlProperty); }
            set { SetValue(TemplateControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TemplateControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemplateControlProperty =
            DependencyProperty.Register("TemplateControl", typeof(object), typeof(CloseableContainer), new PropertyMetadata(0));


    }
}
