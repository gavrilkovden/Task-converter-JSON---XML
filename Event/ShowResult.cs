using System;
using System.Collections.Generic;
using System.Text;

namespace Event
{
   public class ResultShow
    {
        public void Message(int numb, int iterator)
        {
            Console.WriteLine($"A prime number {numb} has been deected. A number {numb} in the Fibonacci sequence - {iterator}");
        }
    }
}
