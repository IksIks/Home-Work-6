using System;
using System.IO;

namespace GroupsOfNumbers
{
    class Program
    {
        /// <summary>
        /// Функция преобразующая число из файла в последовательность чисел, по длинне, равную числу из файла
        /// </summary>
        /// <param name="nFromFile"></param>
        /// <returns></returns>
        public static int[] GetMassivN(string nFromFile)
        {

            int[] getMassivN = new int[int.Parse(nFromFile)];
            for (int i = 1; i <= getMassivN.Length; i++)
            {
                getMassivN[i - 1] = i;
            }
            return getMassivN;
        }
        /// <summary>
        /// Функция в которой если делимое делиться на делитель, то делимое заменяется на 0.
        /// исходный массив возвращается обновленным
        /// </summary>
        /// <param name="massiv"></param>
        /// <returns></returns>
        public static int[] SortMassiv(int[] massiv)
        {
            int[] massivForSort = new int[massiv.Length];       //новый массив
            Array.Copy(massiv, massivForSort, massiv.Length);   //делаю копию (не присвоение) исходного массива massiv, так как он ссылочний тип и при изменении
                                                                //нового массива меняется и исходный
            Array.Clear(massiv, 0, massiv.Length);
            for (int i = 1; i < massivForSort.Length; i++)
            {
                for (int j = 0; j < i; j++)     //цикл проверки всех чисел j начиная с первого до i-ой позиции
                {
                    if (massivForSort[j] == 1)  //убираем единицу т.к. на неё делятся все числа
                        massivForSort[j] = 0;
                    if (massivForSort[j] != 0 && massivForSort[i] != 0) //делить на ноль - плохо, пропускаем эти элементы
                    {
                        if (massivForSort[i] % massivForSort[j] == 0)
                        {
                            massiv[i] = massivForSort[i];       //делимое перемещается в исходный массив massiv на i-тую позицию
                            massivForSort[i] = 0;
                        }
                    }
                }
            }
            Array.Sort(massiv);
            Console.WriteLine(massiv.Length);
            return massivForSort;       //возврат функции это массив вида {0,0,3,5,0,7 и т.д.}
                                        //одновременно идет возврат massiv для повторной сортировки этого массива
        }
        /// <summary>
        /// Функция сортирует массив и удаляет все нули
        /// </summary>
        /// <param name="massivForSort"></param>
        /// <returns></returns>
        public static int[] sortingAndDeleteNull(int[] massivForSort)
        {
            Array.Sort(massivForSort);
            int position = 0;
            for (int i = 0; i < massivForSort.Length; i++)  //ищется последние вхождения нуля
            {
                if (massivForSort[i] == 0)
                    position = i + 1;
            }


            int[] massivSorted = new int[massivForSort.Length - position];      //новый массив размерностью за вычетом позиции последнего вхождения нуля
            Array.Copy(massivForSort, position, massivSorted, 0, massivSorted.Length);  //копирование отсортированного массива с нулями
                                                                                        //в новый с позции следующей после последнего вхожднея нуля

            return massivSorted;    //массив вида {3,5,7,11 и т.д}
        }

        public static void Main()
        {
            File.WriteAllText(@"test.txt", "100");
            string nFromFile = File.ReadAllText(@"test.txt");
            DateTime start = DateTime.Now;
            int[] massivN = GetMassivN(nFromFile);
            int[][] groups = new int[(int)Math.Log(massivN.Length, 2) + 1][]; //честно спер с инета, т.к. это уже посчитаное значение
                                                                              //размера массива от количества N
            int m = 1;      //учет количества групп скомпанованых чисел

            groups[0] = new int[] { 1 };    //первый массив всегда будет с единицей

            for (int i = 1; i < groups.Length; i++)
            {
                massivN = sortingAndDeleteNull(massivN);
                groups[i] = sortingAndDeleteNull(SortMassiv(massivN));
                m++;
            }
            TimeSpan end = DateTime.Now.Subtract(start);
            Console.WriteLine("Как вывести результат ?" +
            "\nв файл - нажмите 'F'\nили количество групп на экран - нажмите 'G'");

            char chois = Char.ToUpper(Console.ReadKey(true).KeyChar);
            while (chois != 'F' && chois != 'G')
            {
                Console.WriteLine("'F' или 'G'");
                chois = Char.ToUpper(Console.ReadKey(true).KeyChar);
            }
            switch (chois)
            {
                case 'G':
                    Console.Write($"Колличество груп {m}");
                    break;
                case 'F':
                    using (StreamWriter sr = new StreamWriter("testEnd.txt")) //создание потока для записи массива массивов в файл
                    {
                        for (int i = 0; i < groups.Length; i++)
                        {
                            for (int j = 0; j < groups[i].Length; j++)
                            {
                                sr.Write($"{groups[i][j]}  ");
                            }
                            sr.WriteLine();
                        }
                    }
                    Console.WriteLine("Программа завершена,откройте файл с данными");
                    Console.WriteLine($"Время: {end.Minutes} минут, {end.Seconds} секуннд, {end.Milliseconds} миллисекунд");
                    break;

            }
            Console.ReadKey();
        }
    }
}
