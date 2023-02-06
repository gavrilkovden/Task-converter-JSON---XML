using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Event
{
    public class Reader
    {
        public delegate void PrimeNumber(int number, int iterator);
        public event PrimeNumber OnPrimeNumber;

        public void Read(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string numberString = reader.ReadToEnd();
                string[] numbersArray = numberString.Split(',');

                for (int a = 3; a < numbersArray.Length; a++)
                {
                    int number = int.Parse(numbersArray[a]);
                    bool isPrime = IsPrimeNumber(number);
                    if (isPrime)
                    {
                        OnPrimeNumber(number, a);
                    }
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
