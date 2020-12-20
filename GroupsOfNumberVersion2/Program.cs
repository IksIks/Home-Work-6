using System;
using System.IO;

namespace GroupsOfNumberVersion2
{
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllText(@"number.txt", "50");
            int numberFromFile = int.Parse(File.ReadAllText(@"number.txt"));
            int M = (int)Math.Log(numberFromFile, 2) + 1;
        }
    }
}
