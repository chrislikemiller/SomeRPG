using Somerpg.Client.Actions;
using Somerpg.Common.Model;
using Somerpg.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Somerpg.View.Converters
{
    class RequiredResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is (Material Required, Material Current))
            {
                return parameter.ToString() switch
                {
                    "Wood" => $"{Required.Wood} / {Current.Wood}",
                    "Metal" => $"{Required.Metal} / {Current.Metal}",
                    "Leather" => $"{Required.Leather} / {Current.Leather}",
                    _ => throw new ArgumentException()
                };
            }
            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
