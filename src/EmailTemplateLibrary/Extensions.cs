using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EmailTemplateLibrary
{
    public static class Extensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly DateTime MillisecondTimestampBoundaryDate = new DateTime(1978, 1, 11, 21, 31, 40, 799, DateTimeKind.Utc);
        private static readonly long MillisecondTimestampBoundary = 253402300799L;

        public static long ToTimestamp(this DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalSeconds;
        }

        public static DateTime FromTimestamp(this long value)
        {
            return Epoch.AddSeconds(value);
        }

        public static long ToMillisecondTimestamp(this DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalMilliseconds;
        }

        public static DateTime FromMillisecondTimestamp(this long value)
        {
            return Epoch.AddMilliseconds(value);
        }

        public static string SerializeDateTime(this DateTime value)
        {
            if (value > MillisecondTimestampBoundaryDate && value < DateTime.MaxValue)
            {
                return ToMillisecondTimestamp(value).ToString("D", CultureInfo.InvariantCulture);
            }

            return value.ToString("O", CultureInfo.InvariantCulture);
        }

        public static DateTime DeserializeDateTime(string value)
        {
            if (long.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out var timestamp))
            {
                return timestamp > MillisecondTimestampBoundary
                    ? FromMillisecondTimestamp(timestamp)
                    : FromTimestamp(timestamp);
            }

            return DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        }

        public static DateTime? DeserializeNullableDateTime(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return null;
            }

            return DeserializeDateTime(value);
        }
    }
}
