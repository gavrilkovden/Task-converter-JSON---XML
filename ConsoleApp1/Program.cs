using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {

        static string currentDir = Directory.GetCurrentDirectory();
       static string pathOfNumbers = Path.GetFullPath(Path.Combine(currentDir, @"numbers.txt"));
       static public char[] numbersChar;
       static public int number;
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader(pathOfNumbers);
            numbersChar = reader.ReadToEnd().ToCharArray();
         
            foreach (var n in numbersChar)
            {
               // Console.Write(n);
            }
            Console.WriteLine(numbersChar[5]);
            for (int i = 2; i < numbersChar.Length / 2; i++)
            {
                if (numbersChar[i] % i == 0)
                {
                    number = numbersChar[i];
                   // Console.WriteLine(numbersChar[i]);
                }
            }

        }
    }

}
