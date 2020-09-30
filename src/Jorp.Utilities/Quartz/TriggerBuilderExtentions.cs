using System;

namespace Jorp.Utilities.Quartz
{
    public static class TriggerBuilderExtentions
    {
        public static TriggerBuilder Create(this TriggerBuilder triggerBuilder)
        {
            return new TriggerBuilder();
        }

        public static TriggerBuilder WithIdentity(this TriggerBuilder triggerBuilder, params string[] paramStrings)
        {
            triggerBuilder.Identity.AddRange(paramStrings);
            return triggerBuilder;
        }

        public static TriggerBuilder WithCronSchedule(this TriggerBuilder triggerBuilder, string cronSchedule)
        {
            triggerBuilder.CronSchedule = cronSchedule;
            return triggerBuilder;
        }

        public static TriggerBuilder InTimeZone(this TriggerBuilder triggerBuilder, TimeZoneInfo timeZoneInfo)
        {
            triggerBuilder.TimeZoneInfo = timeZoneInfo;
            return triggerBuilder;
        }

    }
}