using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Event
{
    public class Reader
    {
        public string numberString;
        public string[] numbersArray;
        public delegate void PrimeNumber(int numberFibonacci, int iterator);
        public event PrimeNumber OnPrimeNumber;

        public void Read(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                numberString = reader.ReadToEnd();
            }

            numbersArray = numberString.Split(',');

            for (int a = 3; a < numbersArray.Length; a++)
            {
                int number = int.Parse(numbersArray[a]);
                bool result = IsPrimeNumber(number);
                if (result)
                {
                    OnPrimeNumber(number, a);
                }
            }
        }

        public bool IsPrimeNumber(int numberFibonacci)
        {
            for (int i = 2; i < numberFibonacci; i++)
            {
                if (numberFibonacci % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
