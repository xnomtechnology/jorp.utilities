using System;
using System.Collections.Generic;
using System.Text;

namespace Jorp.Utilities.Quartz
{
    public class TriggerBuilder
    {
       
        internal List<string> Identity { get; set; }
        internal string CronSchedule { get; set; }
        internal TimeZoneInfo TimeZoneInfo { get; set; }

    }
}
