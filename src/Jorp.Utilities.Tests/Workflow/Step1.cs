using System;
using Jorp.Utilities.Workflow;

namespace Jorp.Utilities.Tests.Extentions
{
    public class Step1 : IStep
    {
        public Exception InnerExceptions { get; set; }
        public StepSettings StepSettings { get; set; }

        public void Execute()
        {
            //Execute custom code
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("step1:" + DateTime.Now.ToString());
        }
    }
}