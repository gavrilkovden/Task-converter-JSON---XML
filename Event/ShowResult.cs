using System;
using System.Collections.Generic;
using System.Text;

namespace Event
{
   public class SearchInformation
    {
        public void OutputOfResult(int numb, int index)
        {
            Console.WriteLine($"A prime number {numb} has been deected. A number {numb} in the Fibonacci sequence - {index}");
        }
    }
}
