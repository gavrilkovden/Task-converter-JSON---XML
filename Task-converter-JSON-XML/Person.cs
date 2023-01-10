using System;
using System.Collections.Generic;
using System.Text;

namespace Task_converter_JSON_XML
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateTimePerson { get; set; }
        public Person() { }

        public Person(string name, string lastname, int year, int month, int day)
        {
            Name = name;
            LastName = lastname;
            DateTimePerson = new DateTime(year, month, day);
        }
        public override string ToString()
        {
            return $"{Name} {LastName} {DateTimePerson}";
        }
    }
}
