using System;
using System.Collections.Generic;
using System.Text;

namespace Jorp.Utilities.Extentions
{
    public static class SchedulerExtention
    {
        public static bool Trigger(this DateTime dateTime, DateTime triggerDateTime)
            => dateTime.Date.CompareTo(triggerDateTime.Date) == 0 && 
            dateTime.TimeOfDay.CompareTo(triggerDateTime.TimeOfDay) == 0;
        
    }
}
