using System;
using System.IO;

namespace GroupsOfNumberVersion2
{
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllText(@"number.txt", "50");
            string numberFromFile = File.ReadAllText(@"number.txt");

        }
    }
}
