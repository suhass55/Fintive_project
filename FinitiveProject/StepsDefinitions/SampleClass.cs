using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Finitve.StepsDefinitions
{
    [Binding]
    class SampleClass
    {


        [Given(@"the first number is (.*)")]
        [Obsolete]
        public void GivenTheFirstNumberIs(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"the second number is (.*)")]
        [Obsolete]
        public void GivenTheSecondNumberIs(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"the two numbers are added")]
        [Obsolete]
        public void WhenTheTwoNumbersAreAdded()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int p0)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
