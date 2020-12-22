using System;
using System.IO;

namespace GroupsOfNumberVersion2
{
    class Program
    {
        static int ExpNumber(int N)
        {
            return (int)Math.Pow(2, N);           
        }
        static void Main(string[] args)
        {
            File.WriteAllText(@"number.txt", "5000");
            int numberFromFile = int.Parse(File.ReadAllText(@"number.txt"));
            //int M = (int)Math.Log(numberFromFile, 2) + 1;
            int minExp = 1;
            int maxExp = ExpNumber(1);
            int count = 1;
            DateTime start = DateTime.Now;
            using StreamWriter numbersWriter = new StreamWriter("test.txt");
            {
                for (int i = 1; i <= numberFromFile; i++)
                {
                    if (i >= minExp && i < maxExp)                        
                        numbersWriter.Write($"{i}  ", true);
                    else
                    {
                        numbersWriter.WriteLine();
                        numbersWriter.Write($"{i}  ", true);
                        count++;
                        minExp = maxExp;
                        maxExp = ExpNumber(count);
                    }
                }
            }
            TimeSpan ts = DateTime.Now.Subtract(start);
            Console.WriteLine($"Время: {ts.Minutes} минут, {ts.Seconds} секуннд, {ts.Milliseconds} миллисекунд");
            Console.ReadKey();
        }
    }
}
