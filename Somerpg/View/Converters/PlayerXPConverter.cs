using Somerpg.Common.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Somerpg.View.Converters
{
    class PlayerXPConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Player player)
            {
                var nextLevelXP = player.GetXPForNextLevel(player.Level);
                return $"{Math.Floor(player.XP / (double)nextLevelXP * 100)}% ({player.XP} / {nextLevelXP})";
            }
            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
