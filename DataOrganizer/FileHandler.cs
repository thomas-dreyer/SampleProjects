using System;
using System.Collections.Generic;
using System.IO;

namespace DataOrganizer
{
    /// <summary>
    /// Handles basic file operations. Such as reading result sets from .CSV files
    /// and writing sorted results to file.
    /// </summary>
    public static class FileHandler
    {
        #region Public Functions

        /// <summary>
        /// Reads a CSV file and returns a Result Set.
        /// </summary>
        /// <param name="filePath">The filename / path where the file may be found.</param>
        /// <param name="fileHeader">The line position where the column headers are specified.</param>
        /// <returns>A Result set containing the file data.</returns>
        public static ResultSet ReadCSV(string filePath, int fileHeader = 0)
        {
            List<string[]> result = new List<string[]>();
            Dictionary<string, int> header = new Dictionary<string, int>();
            string[] filedata = null;
            try
            {
                filedata = File.ReadAllLines(filePath);
                string[] headerData = null;

                for (int index = 0; index < filedata.Length; index++)
                {
                    if (index == fileHeader)
                    {
                        headerData = filedata[index].Split(',');

                        for (int i = 0; i < headerData.Length; i++)
                        {
                            header.Add(headerData[i], i);
                        }
                    }
                    else
                    {
                        result.Add(filedata[index].Split(','));
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occurred {0}", ex.Message);
            }
            return new ResultSet() { Header = header, Data = result };
        }

        /// <summary>
        /// Write result list to file.
        /// </summary>
        /// <param name="filePath">Path of the file.</param>
        /// <param name="result">The result set to write to file.</param>
        public static void WriteResult(string filePath, List<string> result)
        {
            try
            {
                File.WriteAllLines(filePath, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

    }
}
