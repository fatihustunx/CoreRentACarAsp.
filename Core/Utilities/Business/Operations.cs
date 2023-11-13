using Core.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class Operations
    {
        public static DateTime Parser(string dateString)
        {
            DateTime rentDay;
            DateTime.TryParseExact(dateString, Format.RentalDateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out rentDay);

            return rentDay;
        }

        public static bool IsParser(string dateString)
        {
            DateTime dateTime;
            return DateTime.TryParseExact(dateString, Format.RentalDateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
        }
    }
}
