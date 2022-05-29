using Datex.DeliveryConformation.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Datex.DeliveryConformation.WPF.Helpers.Converters
{
    internal class ShipmentStatusToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null && parameter == null)
                return true;

            //if (!(value is ShipmentStatuses) || !(parameter is ShipmentStatuses))
            //    return false;

            //ShipmentStatuses status = (ShipmentStatuses)value;
            //ShipmentStatuses param = (ShipmentStatuses)parameter;

            //return status == param;

            return value?.Equals(parameter) ?? false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
