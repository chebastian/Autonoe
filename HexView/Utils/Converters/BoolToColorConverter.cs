using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}
