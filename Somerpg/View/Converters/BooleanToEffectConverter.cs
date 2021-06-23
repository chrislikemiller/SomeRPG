using Somerpg.Client.Actions;
using Somerpg.Common.Model;
using Somerpg.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace Somerpg.View.Converters
{
    class BooleanToEffectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool isToolWindowOpen && isToolWindowOpen
                ? new BlurEffect()
                : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
