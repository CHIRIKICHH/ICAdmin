using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ICAdmin.Converters
{
    internal class StyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Style))
            {
                throw new InvalidOperationException("The target must be a Style");
            }

            string styleValue = value?.ToString();
            if (styleValue == null)
            {
                return null;
            }

            Style newStyle = (Style)Application.Current.TryFindResource(styleValue);
            return newStyle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
