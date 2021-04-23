using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FoodTrack.Converters
{
    class WidhtHeightConverter : IValueConverter
    {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            if (parameter != null)
            {
                string param = parameter.ToString();
                double paramD = double.Parse(param, provider);
                return System.Convert.ToDouble(value) / paramD;
            }
            return value;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return DependencyProperty.UnsetValue;
            }
    }

    class TwoPartConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            if (parameter != null)
            {
                string param = parameter.ToString();
                double paramD = double.Parse(param, provider);
                string returned = ((int)(System.Convert.ToDouble(value) / paramD)).ToString();
                returned += ",6";
                return returned;
            }
            return ((int)value + ",6").ToString();


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    class SizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            if (parameter != null)
            {
                string param = parameter.ToString();
                double paramD = double.Parse(param, provider);
                string returned = ((int)(System.Convert.ToDouble(value) / paramD)).ToString();
                returned += ",";
                returned += ((int)(System.Convert.ToDouble(value) / paramD)).ToString();
                return returned;
            }
            return ((int)value + "," + value).ToString();


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
