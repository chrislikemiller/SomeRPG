using Somerpg.Client.Actions;
using Somerpg.Common.Model;
using Somerpg.Client.Util;
using Somerpg.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Somerpg.View.Converters
{
    class PlusCostToolTipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Item item)
            {
                return $"Cost: {GameConstants.GetNextPlusCost(item.Tier, item.PlusValue):N0}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
