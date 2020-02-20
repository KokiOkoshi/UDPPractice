using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Net;
using System.Globalization;

namespace UDPPractice.Converters
{
    sealed class NullableBooleanInverseConverter : ConverterBase<bool?, bool>
    {
        protected override bool TryConvert(bool? value, out bool result, object parameter, CultureInfo culture)
        {
            if (!value.HasValue)
            {
                result = false;
                return false;
            }

            result = !value.Value;
            return true;
        }

        protected override bool TryConvertBack(bool value, out bool? result, object parameter, CultureInfo culture)
        {
            result = !value;
            return true;
        }
    }
}
