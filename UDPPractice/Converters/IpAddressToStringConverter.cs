using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Net;
using System.Globalization;

namespace UDPPractice.Converters
{
    sealed class IpAddressToStringConverter : ConverterBase<IPAddress, string>
    {
        protected override bool TryConvert(IPAddress value, out string result, object parameter, CultureInfo culture)
        {
            result = value.ToString();
            return true;
        }

        protected override bool TryConvertBack(string value, out IPAddress result, object parameter, CultureInfo culture)
            => IPAddress.TryParse(value, out result);
    }
}
