using System;
using Somerpg.Common.Model;
using System.Globalization;
using System.Windows.Data;

namespace Somerpg.View.Converters
{
    class ItemTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Item item)
            {
                return item switch
                {
                    Armor _ => "Armor",
                    Weapon _ => "Weapon",
                    _ => throw new ArgumentException()
                };
            }
            return "Item";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
