using Somerpg.Client.Actions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Somerpg.View.Converters
{
    class ActionToTimeLeftConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is GameAction gameAction)
            {
                return gameAction.TimeLeft < 0 ? "" : $"({TimeSpan.FromSeconds(gameAction.TimeLeft):c})";
            }
            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
