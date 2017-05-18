using System.Collections.Generic;

namespace DataOrganizer
{
    /// <summary>
    /// Represents a data set read from a CSV file.
    /// </summary>
    public class ResultSet
    {
        #region Properties
        /// <summary>
        /// File header reference.
        /// </summary>
        public Dictionary<string, int> Header { get; set; }
        /// <summary>
        /// Data record reference.
        /// </summary>
        public List<string[]> Data { get; set; }

        #endregion

    }
}
