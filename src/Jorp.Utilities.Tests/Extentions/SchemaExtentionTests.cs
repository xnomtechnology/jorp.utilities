using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Jorp.Utilities.Extentions;

namespace Jorp.Utilities.Tests.Extentions
{
    [TestFixture]
    public class SchemaExtentionTests
    {
        static readonly object[] JsonContentSourceCases =
        {
            new object[] { @"{'ssn': 1212121, 'name':'Saman Payam', 'titel':'System developer'}", 1212121, "Saman Payam", string.Empty },
            new object[] { @"{'ssn': 1212122, 'name':'Banah Payam', 'titel':null}", 1212122, "Banah Payam", string.Empty },
        };

        [Test, TestCaseSource("JsonContentSourceCases")]
        public void FromJsonToModelBaseTest(string jsonString, int ssn, string name, string title)
        {
            var model = jsonString.FromJsonToModel<TestClass>();

            Assert.That(model.SSN, Is.EqualTo(ssn));
            Assert.That(model.Name, Is.EqualTo(name));
        }
    }

    public class TestClass
    {
        public int SSN { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
