using System;

namespace Argis.CalenderSystem.Runtime
{
    /// <summary>
    /// This formats dates or converts string dates to DateTime objects.
    /// 
    /// Chris Anderson - ??? - Initial Creation
    /// Malaika Penn - 2021-07 - Moved to Lens 3.0
    /// </summary>
    public static class DateTimeConverter
    {
        /// <summary>
        /// e.g. "2008-05-01T08:30:52Z";
        /// </summary>
        public const string iso8601Format = "yyyy-MM-ddTHH:mm:ssZ";

        public static string ToIso8601Format(this DateTime dateTime)
        {
            return dateTime.ToString(iso8601Format);
        }

        public static DateTime ConvertIsoStringToDateTime(this string iso8601String)
        {
            return DateTime.ParseExact(iso8601String, iso8601Format,
                                            System.Globalization.CultureInfo.InvariantCulture);
        }

        public static long ConvertDateToJulianDay(this DateTime dateTime)
        {
            return ConvertDateToJulianDay(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public static long ConvertDateToJulianDay(int year, int month, int day)
        {
            long jd;

            jd = day - 32075L + 1461L * (year + 4800L + (month - 14) / 12) / 4;
            jd = jd + 367L * (month - 2 - (month - 14) / 12 * 12) / 12;
            jd = jd - 3 * ((year + 4900L + (month - 14) / 12) / 100) / 4;

            return jd;
        }

        /// <summary>
        /// Esri Epoch
        /// System.DateTime(1970, 1, 1).Ticks
        /// </summary>
        public const long EsriEpoch = 621355968000000000;

        /// <summary>
        /// Convert Esri Tick To DateTime
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static DateTime ConvertEsriTickToDateTime(this string milliseconds)
        {
            if (string.IsNullOrWhiteSpace(milliseconds))
                milliseconds = "0";
            return ConvertEsriTickToDateTime(Convert.ToInt64(milliseconds));
        }

        /// <summary>
        /// Convert Esri Tick To DateTime
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static DateTime ConvertEsriTickToDateTime(this long milliseconds)
        {
            long ticks = (milliseconds * 10000) + EsriEpoch;
            return new DateTime(ticks);
        }

        public static long ConvertDateTimeToEsriTick(this DateTime dateTime)
        {
            return (dateTime.Ticks - EsriEpoch) / 10000;
        }
    }
}