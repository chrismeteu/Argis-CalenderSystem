using System;

namespace Argis.CalenderSystem.Runtime
{
    /// <summary>
    /// Extenstion class for manipulating dates and times.
    /// </summary>
    /// <example>
    /// Here are some examples of ISO 8601 date/time formats:
    ///
    ///    1. Date only(YYYY-MM-DD):
    ///       2023-03-20
    ///    2. Time only(hh:mm:ss):
    ///       14:30:00
    ///       
    ///    3. Date and time(YYYY-MM-DDThh:mm:ss):
    ///       2023-03-20T14:30:00
    /// 
    ///    4. Date and time with time zone(YYYY-MM-DDThh:mm:ssZ or YYYY-MM-DDThh:mm:ss±hh:mm):
    ///       2023-03-20T14:30:00Z(UTC time)
    ///       2023-03-20T14:30:00+02:00 (2 hours ahead of UTC)
    ///       
    ///    5. Date and time with fractional seconds:
    ///       2023-03-20T14:30:00.123456Z
    /// 
    ///    6. Duration(PnYnMnDTnHnMnS):
    ///       P1Y2M3DT4H5M6S(1 year, 2 months, 3 days, 4 hours, 5 minutes, and 6 seconds)
    ///
    ///    7. Week date(YYYY-Www-D):
    ///       2023-W12-1 (Monday of the 12th week of 2023)
    /// </example>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// https://en.wikipedia.org/wiki/ISO_8601
        /// https://www.iso.org/iso-8601-date-and-time-format.html
        /// </summary>
        /// <example>
        /// "2008-05-01T08:30:52Z"
        /// </example>
        public const string iso8601Format = "yyyy-MM-ddTHH:mm:ssZ";

        /// <summary>
        /// Converts System.DateTime object to an iso8601 format.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToIso8601Format(this DateTime dateTime)
        {
            return dateTime.ToString(iso8601Format);
        }

        /// <summary>
        /// Converts an iso8601 formated string to a System.DataTime object.
        /// </summary>
        /// <param name="iso8601String"></param>
        /// <returns></returns>
        public static DateTime ConvertIsoStringToDateTime(this string iso8601String)
        {
            return DateTime.ParseExact(iso8601String, iso8601Format, System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a System.Date object to a Julian Day value.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ConvertDateToJulianDay(this DateTime dateTime)
        {
            return ConvertDateToJulianDay(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        /// <summary>
        /// Return a Julian Day number.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static long ConvertDateToJulianDay(int year, int month, int day)
        {
            long jd;

            jd = day - 32075L + 1461L * (year + 4800L + (month - 14) / 12) / 4;
            jd = jd + 367L * (month - 2 - (month - 14) / 12 * 12) / 12;
            jd = jd - 3 * ((year + 4900L + (month - 14) / 12) / 100) / 4;

            return jd;
        }

        /// <summary>
        /// Esri Epoch value.
        /// System.DateTime(1970, 1, 1).Ticks
        /// </summary>
        public const long EsriEpoch = 621355968000000000;

        /// <summary>
        /// Convert Esri tick value to a System.DateTime
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
        /// Convert Esri tick value to a System.DateTime
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static DateTime ConvertEsriTickToDateTime(this long milliseconds)
        {
            long ticks = (milliseconds * 10000) + EsriEpoch;
            return new DateTime(ticks);
        }


        /// <summary>
        /// Converts a System.DateTime to an Esri tick value.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ConvertDateTimeToEsriTick(this DateTime dateTime)
        {
            return (dateTime.Ticks - EsriEpoch) / 10000;
        }
    }
}