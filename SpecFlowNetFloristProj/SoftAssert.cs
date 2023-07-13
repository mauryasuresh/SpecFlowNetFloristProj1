using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowNetFloristProj
{
    public class SoftAssert
    {
        private List<string> errors = new List<string>();

        public void Fail(string message)
        {
            errors.Add(message);
        }

        public void Pass(string message)
        {
            // Do nothing
        }

        public void AssertAll()
        {
            if (errors.Count > 0)
            {
                string errorMessage = string.Join(Environment.NewLine, errors);
                Assert.Fail(errorMessage);
            }
        }

        public void Info(string message)
        {
            Console.WriteLine(message);
        }
    }
}
