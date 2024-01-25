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
            DateTime resDateTime;

            DateTime.TryParseExact(dateString, Format.newDateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out resDateTime);

            return resDateTime;
        }

        public static bool IsParser(string dateString)
        {
            DateTime dateTime;

            return DateTime.TryParseExact(dateString, Format.newDateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
        }
    }
}