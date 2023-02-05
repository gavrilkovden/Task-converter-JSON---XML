using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Event
{

    public class ReaderFile
    {
        static string currentDir = Directory.GetCurrentDirectory();

        public string numberString;
        public string[] numbersArray;
        static public int number;
        static public int a;
        public delegate void PrimeNumber();

        public event PrimeNumber OnPrimeNumber;

        public void ReaderTxt(string path)
        {
            StreamReader reader = new StreamReader(path);
            numberString = reader.ReadToEnd();
            numbersArray = numberString.Split(',');

            for (a = 3; a < numbersArray.Length; a++)
            {
                number = int.Parse(numbersArray[a]);
                for (int i = 2; i < number; i++)
                {
                    if (number % i == 0)
                    {
                        goto found;
                    }
                }
                OnPrimeNumber();
            found: ;
            }
        }

    }
}
