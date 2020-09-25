using System;

namespace Jorp.Utilities.Models
{
    /// <summary>
    /// Step settings for requried properties
    /// </summary>
    public class StepSettings
    {
        public Status Status { get; set; }
        public Exception InnerExceptions { get; set; }
    }
}