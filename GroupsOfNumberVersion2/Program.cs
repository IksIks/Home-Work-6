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
        static void Print(string text)
        {
            Console.WriteLine(text);
        }
        static void Main(string[] args)
        {
            File.WriteAllText(@"number.txt", "100");
            int numberFromFile = int.Parse(File.ReadAllText(@"number.txt"));
            byte M = Convert.ToByte(Math.Log(numberFromFile, 2));
            Print("Что сделать с даннными:\n'S' - запись в файл" +
                                           "\n'G' - вывод количества групп на экран");
            char symbol = Char.ToUpper(Console.ReadKey(true).KeyChar);
            while ((symbol != 'S') && (symbol != 'G'))
            {
                Print("Вводите либо 'S', либо 'G'");
                symbol = Char.ToUpper(Console.ReadKey(true).KeyChar);
            }
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
            Print($"Время: {ts.Minutes} минут, {ts.Seconds} секуннд, {ts.Milliseconds} миллисекунд");
            Console.ReadKey();
        }
    }
}
