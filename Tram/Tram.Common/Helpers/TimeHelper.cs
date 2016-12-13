using System;

namespace Tram.Common.Helpers
{
    public static class TimeHelper
    {
        public static DateTime GetTime(int hours, int minutes)
        {
            return (new DateTime()).AddHours(hours).AddMinutes(minutes);
        }

        // string format - 'HH:MM'
        public static DateTime GetTime(string timeStr)
        {
            string[] parts = timeStr.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            return (new DateTime()).AddHours(int.Parse(parts[0])).AddMinutes(int.Parse(parts[1]));
        }

        public static string GetTimeStr(DateTime dateTime)
        {
            return dateTime.ToString("HH:mm");
        }
    }
}
