using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace GroupsOfNumberVersion2
{
   class Program
   {
		/// <summary>
		/// Возвращает степень двойки
		/// </summary>
		/// <param name="N">степень</param>
		/// <returns> 2 в степени N </returns>
	  static int ExpNumber(int N)
	  {
		 return (int)Math.Pow(2, N);
	  }

		/// <summary>
		/// Вывод на экран чего угодно
		/// </summary>
		/// <param name="text"></param>
	  static void Print(string text)
	  {
		 Console.WriteLine(text);
	  }

		/// <summary>
		/// Функция создает Zip поток
		/// </summary>
		/// <param name="file"> путь к исходному файлу и его имя </param>
		/// <param name="zipFile"> путь к архивному файлу и его имя </param>
		static void DataCompression(string file, string zipFile)
		{
			using (FileStream read = new FileStream(file, FileMode.OpenOrCreate))
			{
				using (FileStream write = File.Create(zipFile + ".gz"))
				{
					using (GZipStream zip = new GZipStream(write, CompressionMode.Compress))
					{
						read.CopyTo(zip);
					}
				}
			}
		}
		/// <summary>
		/// Функция проверки ввода ответов от пользователя
		/// </summary>
		/// <param name="answer1"> первый вариант ответа </param>
		/// <param name="answer2"> второй вариант ответа </param>
		/// <returns></returns>
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
	  /// <summary>
	  /// Функия определяет размер файла
	  /// </summary>
	  /// <param name="file"> путь к файлу и его имя </param>
	  /// <returns></returns>
	  static long SizeFile(string file)
	  {	
			FileInfo size = new FileInfo(file);
			return size.Length;
	  }

	  static void Main(string[] args)
	  {
		 
		 File.WriteAllText(@"number.txt", "5000");
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
				  
					//string outputZipFile = "testZip.txt.gz";
					Print("Сжать получившиеся данные? Y/N ");
					symbol = CheckingInput('Y', 'N');
					if (symbol == 'Y')
					{
						DataCompression(outputFile, outputFile);
						sizeFile = SizeFile(outputFile + ".gz");
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
