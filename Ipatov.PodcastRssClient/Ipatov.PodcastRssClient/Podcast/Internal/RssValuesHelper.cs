using System;
using System.Globalization;
using System.Xml;

namespace Ipatov.PodcastRssClient.Podcast.Internal
{
    /// <summary>
    /// RSS date helper.
    /// </summary>
    public static class RssValuesHelper
    {
        /// <summary>
        /// Date and time converter.
        /// </summary>
        /// <param name="date">Date as string.</param>
        /// <returns>Date.</returns>
        public static DateTime? ConvertDateTime(string date)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(date))
                {
                    return null;
                }
                date = date.Trim();
                //return XmlConvert.ToDateTime(date, XmlDateTimeSerializationMode.Local);
                DateTime dt = DateTimeOffset.ParseExact(date, "ddd, dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture, DateTimeStyles.None).LocalDateTime;
                return dt;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Integer converter.
        /// </summary>
        /// <param name="intval">Integer as string.</param>
        /// <returns>Integer.</returns>
        public static int? ConvertInteger(string intval)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(intval))
                {
                    return null;
                }
                intval = intval.Trim();
                return XmlConvert.ToInt32(intval);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Integer converter.
        /// </summary>
        /// <param name="intval">Integer as string.</param>
        /// <returns>Integer.</returns>
        public static ulong? ConvertUlong(string intval)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(intval))
                {
                    return null;
                }
                intval = intval.Trim();
                return XmlConvert.ToUInt64(intval);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert duration.
        /// </summary>
        /// <param name="interval">Duration as string.</param>
        /// <returns>Duration.</returns>
        public static TimeSpan? ConvertDuration(string interval)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(interval))
                {
                    return null;
                }
                interval = interval.Trim();
                //return XmlConvert.ToTimeSpan(interval);
                return TimeSpan.Parse(interval);
            }
            catch
            {
                return null;
            }
        }
    }
}