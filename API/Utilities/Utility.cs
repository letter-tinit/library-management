using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utilities
{
    public static class Utility
    {
        public static DateOnly ToDateOnly(string dateString)
        {
            string format = "yyyy-MM-dd";
            var culture = System.Globalization.CultureInfo.InvariantCulture;

            if (DateTime.TryParseExact(dateString, format, culture, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
            {
                DateOnly dateOnly = DateOnly.FromDateTime(dateTime);

                return dateOnly;
            }
            else
            {
                return DateOnly.FromDateTime(DateTime.Now);
            }
        }
    }
}