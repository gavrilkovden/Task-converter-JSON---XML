using System;
using System.Collections.Generic;
using System.Text;

namespace Event
{
   public class ShowResult
    {
        ReaderFile readerFile = new ReaderFile();
        public void Message()
        {
            Console.WriteLine($"A prime number {ReaderFile.number} has been deected. A number {ReaderFile.number} in the Fibonacci sequence - {ReaderFile.a}");
        }
    }
}
