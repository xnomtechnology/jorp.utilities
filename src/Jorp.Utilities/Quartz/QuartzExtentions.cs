using System;

namespace Jorp.Utilities.Quartz
{
    public static class QuartzExtentions
    {
        public static QuartzBuilder Create(this QuartzBuilder triggerBuilder)
        {
            triggerBuilder = new QuartzBuilder();
            return triggerBuilder;
        }

        public static QuartzBuilder WithIdentity(this QuartzBuilder quartzBuilder, params string[] paramStrings)
        {
            if (quartzBuilder == null) quartzBuilder.Create();;
            
            quartzBuilder.Identity.AddRange(paramStrings);
            return quartzBuilder;
        }

        public static QuartzBuilder WithCronSchedule(this QuartzBuilder quartzBuilder, string cronSchedule)
        {
            if (quartzBuilder == null) quartzBuilder.Create(); ;

            quartzBuilder.CronSchedule = cronSchedule;
            return quartzBuilder;
        }

        public static QuartzBuilder InTimeZone(this QuartzBuilder quartzBuilder, TimeZoneInfo timeZoneInfo)
        {
            if (quartzBuilder == null) quartzBuilder.Create(); ;

            quartzBuilder.TimeZoneInfo = timeZoneInfo;
            return quartzBuilder;
        }

        public static bool Trigger(this QuartzBuilder quartzBuilder)
        {
            //var exp = new Cron
            //TODOS

            return false;
        }

        public static QuartzBuilder Validate(this QuartzBuilder quartzBuilder)
        {
            return quartzBuilder;
        }
    }
}