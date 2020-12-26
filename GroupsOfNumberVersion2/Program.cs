using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

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

	  static void DataCompression(string file, string zipFile)
	  {
		 using (FileStream read = new FileStream(file, FileMode.OpenOrCreate))
		 {
			using (FileStream write = File.Create(zipFile))
			{
			   using (GZipStream zip = new GZipStream(write, CompressionMode.Compress))
			   {
				  read.CopyTo(zip);
			   }
			}
		 }
	  }
	  static char CheckingInput(char answer1, char answer2)
	  {
		 char symbol = Char.ToUpper(Console.ReadKey(true).KeyChar);
		 while ((symbol != answer1) && (symbol != answer2))
		 {
			Print($"Либо {answer1}, либо {answer2}");
			symbol = Char.ToUpper(Console.ReadKey(true).KeyChar);
		 }
		 return symbol;
	  }
	  
	  static long SizeFile(string file)
	  {
	  		FileInfo size = new FileInfo(file);
			return size.Length;
	  }

	  static void Main(string[] args)
	  {
	  	 
		 File.WriteAllText(@"number.txt", "500");
		 int numberFromFile = int.Parse(File.ReadAllText(@"number.txt"));
		 string outputFile = "test.txt";
		 Print("Что сделать с даннными:\n'S' - запись в файл" +
										"\n'G' - вывод количества групп на экран");
		 char symbol = CheckingInput('S', 'G');
		 switch (symbol)
		 {
			case 'S':
				{
				  int minExp = 1;
				  int maxExp = ExpNumber(1);
				  int count = 1;
				  DateTime start = DateTime.Now;
				  using (StreamWriter numbersWriter = new StreamWriter(outputFile))
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
				  Print($"Время: {ts.Minutes} минут, {ts.Seconds} секунд, {ts.Milliseconds} миллисекунд");
				  long sizeFile = SizeFile(outputFile);
				  Print($"Размер файла {outputFile} {sizeFile} byte" );
				  
				  string outputZipFile = "testZip.zip";
				  Print("Сжать получившиеся данные? Y/N ");
				  symbol = CheckingInput('Y', 'N');
				  if (symbol == 'Y')
					 {
					 	DataCompression(outputFile, outputZipFile);
					 	sizeFile = SizeFile(outputZipFile);
					 	Print($"Размер файла {outputFile} после сжатия {sizeFile} byte" );
					 }
				  else
				  {
				  		Print("Конец программы");
				  		break;
				  }
			   }
			   break;
			case 'G':
			   {
				  byte M = Convert.ToByte(Math.Log(numberFromFile, 2));
				  Print($"Количество групп {M}\n Конец программы");
			   }
			   break;
		 }
		 Console.ReadKey();
	  }
   }
}
