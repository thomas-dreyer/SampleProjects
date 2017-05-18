using DataOrganizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DataOrganizer.Tests
{
    [TestClass()]
    public class UnitTests
    {
        private ResultSet testResultSet;

        private void populateTestData()
        {
            if (testResultSet == null)
            {
                testResultSet = new ResultSet()
                {
                    Header = new System.Collections.Generic.Dictionary<string, int>(),
                    Data = new System.Collections.Generic.List<string[]>()
                };
                // populate expected result set
                // header
                testResultSet.Header.Add("FirstName", 0);
                testResultSet.Header.Add("Address", 1);
                testResultSet.Header.Add("LastName", 2);
                // data
                testResultSet.Data.Add(new string[] { "Joe", "1 Gold street", "Moore" });
                testResultSet.Data.Add(new string[] { "Joe", "2 Silver street", "Doe" });
                testResultSet.Data.Add(new string[] { "Jill", "4 Bronze street", "Moorecroft" });
                testResultSet.Data.Add(new string[] { "Jane", "14 Alligator street", "Doe" });
                testResultSet.Data.Add(new string[] { "Hannah", "65 Zebra street", "Smith" });
            }
        }

        [TestMethod()]
        public void SortByColumnSectionTest()
        {
            populateTestData();
            List<string> expectedResult = new List<string>();
            expectedResult.Add("14 Alligator street");
            expectedResult.Add("4 Bronze street");
            expectedResult.Add("1 Gold street");
            expectedResult.Add("2 Silver street");
            expectedResult.Add("65 Zebra street");
            List <string> result = Organizer.SortByColumnSection(testResultSet, "Address", 1);
            for (int index = 0; index < expectedResult.Count; index++)
            {
                if (expectedResult[index] != result[index])
                {
                    Assert.Fail("Sorting by section incorrect.");
                }
            }
        }

        [TestMethod()]
        public void SortByColumnFrequencyTest()
        {
            populateTestData();
            List<string> expectedResult = new List<string>();
            expectedResult.Add("Joe, 2");
            expectedResult.Add("Hannah, 1");
            expectedResult.Add("Jane, 1");
            expectedResult.Add("Jill, 1");
            
            List<string> result = Organizer.SortByColumnFrequency(testResultSet, "FirstName");
            for (int index = 0; index < expectedResult.Count; index++)
            {
                if (expectedResult[index] != result[index])
                {
                    Assert.Fail("Sorting by single column frequency incorrect.");
                }
            }
        }

        [TestMethod()]
        public void SortByMultipleColumnFrequencyTest()
        {
            populateTestData();
            List<string> expectedResult = new List<string>();
            expectedResult.Add("Doe, 2");
            expectedResult.Add("Joe, 2");
            expectedResult.Add("Hannah, 1");
            expectedResult.Add("Jane, 1");
            expectedResult.Add("Jill, 1");
            expectedResult.Add("Moore, 1");
            expectedResult.Add("Moorecroft, 1");
            expectedResult.Add("Smith, 1");
            List<string> result = Organizer.SortByMultipleColumnFrequency(testResultSet, new string[] { "FirstName", "LastName" });
            for (int index = 0; index < expectedResult.Count; index++)
            {
                if (expectedResult[index] != result[index])
                {
                    Assert.Fail("Sorting by multiple column frequency incorrect.");
                }
            }
        }
    }
}

