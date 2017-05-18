using System;
using System.Collections.Generic;
using System.Linq;

namespace DataOrganizer
{
    /// <summary>
    /// Helps to sort data using specific criteria.
    /// </summary>
    public static class Organizer
    {
        #region Public Functions

        /// <summary>
        /// Sorts data ascending by splitting the column where spaces occur.
        /// The data is then sorted by the specified section index.
        /// </summary>
        /// <param name="header">The columns of the ResultSet.</param>
        /// <param name="values">The data of the ResultSet.</param>
        /// <param name="column">The column on which the sorting should occur.</param>
        /// <param name="section">The section index of the column to be used for sorting.</param>
        /// <returns></returns>
        public static List<string> SortByColumnSection(Dictionary<string, int> header, List<string[]> values, string column, int section = 0)
        {
            List<string> result = new List<string>();
            List<KeyValuePair<string, string>> templist = new List<KeyValuePair<string, string>>();
            if (header.ContainsKey(column))
            {
                foreach (string[] line in values)
                {
                    string _line = line[header[column]];
                    string[] recordSection = _line.Split(' ');
                    if (section < recordSection.Length)
                    {
                        KeyValuePair<string, string> entry = new KeyValuePair<string, string>(recordSection[section], _line);
                        templist.Add(entry);
                    }
                }
                var sorted = templist.OrderBy(k => k.Key);
                foreach (var line in sorted)
                {
                    result.Add(line.Value);
                }
            }
            else
            {
                throw new Exception(string.Format("The result set does not contain the column '{0}'", column));
            }
            return result;
        }

        /// <summary>
        /// Sorts data ascending by splitting the column where spaces occur.
        /// The data is then sorted by the specified section index.
        /// </summary>
        /// <param name="resultSet">The result set on which the sorting should occur.</param>
        /// <param name="column">The column on which the sorting should occur.</param>
        /// <param name="section">The section index of the column to be used for sorting.</param>
        /// <returns></returns>
        public static List<string> SortByColumnSection(ResultSet resultSet, string column, int section = 0)
        {
            return SortByColumnSection(resultSet.Header, resultSet.Data, column, section);
        }

        /// <summary>
        /// Counts the frequency of values within the data for a given column.
        /// </summary>
        /// <param name="header">The columns of the ResultSet.</param>
        /// <param name="values">The data of the ResultSet.</param>
        /// <param name="column">The column of the ResultSet on which the counting should be applied.</param>
        /// <returns></returns>
        public static List<string> SortByColumnFrequency(Dictionary<string, int> header, List<string[]> values, string column)
        {
            List<string> result = new List<string>();
            SortedDictionary<string, int> firstSort = new SortedDictionary<string, int>();
            if (header.ContainsKey(column))
            {
                for (int index = 0; index < values.Count; index++)
                {
                    keepCount(firstSort, values[index][header[column]]);
                }
                var sorted = firstSort.OrderByDescending(x => x.Value).ThenBy(x => x.Key);
                foreach (KeyValuePair<string, int> row in sorted)
                {
                    result.Add(string.Format("{0}, {1}", row.Key, row.Value));
                }
            }
            else
            {
                throw new Exception(string.Format("The column {0} is not present in the result set.", column));
            }
            return result;
        }

        /// <summary>
        /// Counts the frequency of values within the data for a given column.
        /// </summary>
        /// <param name="resultSet">The ResultSet</param>
        /// <param name="column">The column of the ResultSet on which the counting should be applied.</param>
        /// <returns></returns>
        public static List<string> SortByColumnFrequency(ResultSet resultSet, string column)
        {
            return SortByColumnFrequency(resultSet.Header, resultSet.Data, column);
        }

        /// <summary>
        /// Counts the frequency of values within the data for a given columns.
        /// </summary>
        /// <param name="header">The columns of the ResultSet.</param>
        /// <param name="values">The data of the ResultSet.</param>
        /// <param name="columns">The columns of the ResultSet on which the counting should be applied.</param>
        /// <returns></returns>
        public static List<string> SortByMultipleColumnFrequency(Dictionary<string, int> header, List<string[]> values, string[] columns)
        {
            List<string> result = new List<string>();
            SortedDictionary<string, int> firstSort = new SortedDictionary<string, int>();
            List<string> missingColumns = new List<string>();
            foreach (string column in columns)
            {
                if (!header.ContainsKey(column))
                {
                    missingColumns.Add(column);
                }
            }
            if (missingColumns.Count == 0)
            {
                for (int index = 0; index < values.Count; index++)
                {
                    for (int columnIndex = 0; columnIndex < columns.Length; columnIndex++)
                    {
                        keepCount(firstSort, values[index][header[columns[columnIndex]]]);
                    }
                }
                firstSort.OrderByDescending(x => x.Value).ThenBy(x => x.Key);
                var sorted = firstSort.OrderByDescending(x => x.Value).ThenBy(x => x.Key);
                foreach (KeyValuePair<string, int> row in sorted)
                {
                    result.Add(string.Format("{0}, {1}", row.Key, row.Value));
                }
            }
            else
            {
                throw new Exception(string.Format("The result set does not contain column(s) : {0}", string.Join(", ", missingColumns)));
            }
            return result;
        }

        /// <summary>
        /// Counts the frequency of values within the data for a given columns.
        /// </summary>
        /// <param name="resultSet">The ResultSet</param>
        /// <param name="columns">The columns of the ResultSet on which the counting should be applied.</param>
        /// <returns></returns>
        public static List<string> SortByMultipleColumnFrequency(ResultSet resultSet, string[] columns)
        {
            return SortByMultipleColumnFrequency(resultSet.Header, resultSet.Data, columns);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Helps to keep count of the frequency of a word.
        /// </summary>
        /// <param name="dictionary">The dictionary used to keep count.</param>
        /// <param name="value">The key value of the entry.</param>
        private static void keepCount(SortedDictionary<string, int> dictionary, string value)
        {
            if (!dictionary.ContainsKey(value))
            {
                dictionary.Add(value, 1);
            }
            else
            {
                dictionary[value]++;
            }
        }


        #endregion

    }
}
