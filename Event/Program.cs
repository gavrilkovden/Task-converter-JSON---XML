using System;
using System.IO;

namespace Event
{
    //  Программа для поиска простых чисел среди чисел Фибоначчи.Числа читаются из файла numbers.txt, 
    //   где записаны в строку через запятую. При нахождении очередного простого числа активировать событие OnPrimeNumber, 
    //  в обработчике которого вывести это число на консоль, а также номер этого числа в последовательности Фибоначчи.
    class Program
    {
        static readonly string currentDir = Directory.GetCurrentDirectory();
        static readonly string pathOfNumbers = Path.GetFullPath(Path.Combine(currentDir, @"numbers.txt"));

        static void Main(string[] args)
        {
            Reader reader = new Reader();
            Information searchInformation = new Information();
            reader.OnPrimeNumber += searchInformation.ShowResult;
            reader.Read(pathOfNumbers);
        }
    }
}
