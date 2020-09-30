using System;
using Jorp.Utilities.Workflow;

namespace Jorp.Utilities.Tests.Extentions
{
    public class Step2 : IStep
    {
        public Exception InnerExceptions { get; set; }
        public StepSettings StepSettings { get; set; }

        public void Execute()
        {
            //Execute custom code
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("step2: " + DateTime.Now.ToString());
        }
    }
}