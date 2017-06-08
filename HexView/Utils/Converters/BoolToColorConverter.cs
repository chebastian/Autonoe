using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HexView.Utils.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public String TrueValue { get; set; }
        public String FalseValue { get; set; }

        public BoolToColorConverter()
        {
            TrueValue = "Red";
            FalseValue = "Blue";
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)(value) ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class BoolToVisible : IValueConverter
    { 
        public BoolToVisible()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class BoolToHidden : IValueConverter
    { 
        public BoolToHidden()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
