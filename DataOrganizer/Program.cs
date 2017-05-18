using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataOrganizer
{
    class Program
    {
        static void Main(string[] args)
        {
            ResultSet result = FileHandler.ReadCSV("data.csv");

            List<string> sorted = Organizer.SortByMultipleColumnFrequency(result, new string[] { "FirstName", "LastName" });
            foreach (string line in sorted)
            {
                Console.WriteLine(line);
            }
            Console.Write("Writing to File...");
            FileHandler.WriteResult("resultfile1.csv", sorted);
            Console.WriteLine("Complete.");
            Console.WriteLine();
            List<string> addresses = Organizer.SortByColumnSection(result, "Address", 1);
            foreach (string line in addresses)
            {
                Console.WriteLine(line);
            }
            Console.Write("Writing to File...");
            FileHandler.WriteResult("resultfile2.csv", addresses);
            Console.WriteLine("Complete.");
            Console.ReadLine();

        }
    }
}
