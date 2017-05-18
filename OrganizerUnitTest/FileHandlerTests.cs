using DataOrganizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace DataOrganizer.Tests
{
    [TestClass()]
    public class FileHandlerTests
    {
        private ResultSet expectedResultSet;

        private void populateTestData()
        {
            if (expectedResultSet == null)
            {
                expectedResultSet = new ResultSet()
                {
                    Header = new System.Collections.Generic.Dictionary<string, int>(),
                    Data = new System.Collections.Generic.List<string[]>()
                };
                // populate expected result set
                // header
                expectedResultSet.Header.Add("PetName", 0);
                expectedResultSet.Header.Add("PetAge", 1);
                expectedResultSet.Header.Add("PetHealth", 2);
                // data
                expectedResultSet.Data.Add(new string[] { "Gandalf", "1", "Excellent" });
                expectedResultSet.Data.Add(new string[] { "Beatrice", "2", "Poor" });
                expectedResultSet.Data.Add(new string[] { "Trinity", "4", "Good" });
            }
        }

        [TestMethod()]
        public void ReadCSVTest()
        {
            populateTestData();

            // read the actual data
            ResultSet resultSet = FileHandler.ReadCSV("pets.csv");

            // compare the results
            if (expectedResultSet.Header.Count == resultSet.Header.Count)
            {
                if (expectedResultSet.Data.Count != resultSet.Data.Count)
                {
                    Assert.Fail("Data test failed.\nExpected {0} rows, result {1} rows.", expectedResultSet.Data.Count, resultSet.Data.Count);
                }
            }
            else
            {
                Assert.Fail("Header line test failed.\nExpected {0} columns, result {1} columns.", expectedResultSet.Header.Count, resultSet.Header.Count);
            }
            

        }

        [TestMethod()]
        public void WriteResultTest()
        {
            List<string> outputData = new List<string>()
            {
               "Line1",
               "Line2",
               "End"
            };
            // write data
            FileHandler.WriteResult("output.data", outputData);

            if (!File.Exists("output.data"))
            {
                Assert.Fail("Failed to write file.");
            }
            else
            {
                string[] outputDataRead = File.ReadAllLines("output.data");
                if (outputData.Count == outputDataRead.Length)
                {
                    for (int index = 0; index < outputDataRead.Length; index++)
                    {
                        if (outputDataRead[index] != outputData[index])
                        {
                            Assert.Fail("Data inconsistent at line {0}", index);
                        }
                    }
                }
                else
                {
                    Assert.Fail("Data inconsistent.");
                }
            }
        }
    }
}