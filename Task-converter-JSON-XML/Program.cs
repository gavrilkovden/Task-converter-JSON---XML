using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Task_converter_JSON_XML
{
    class Program
    {

        //string currentDirJSON = @"JSON";
        //string currentDirXML = @"XML";
        public static string pathJsonFile;
        static async Task Main(string[] args)
        {

            string Exit = null;
            List<Person> storageJsonFile = new List<Person>();
            do
            {
                int counter=0;
                Console.WriteLine("The program is running. To terminate the program enter q");
               Exit = Console.ReadLine();
                var currentDir = Directory.GetCurrentDirectory();
                var JsonPath = Path.GetFullPath(Path.Combine(currentDir, @"JSON"));
                var XmlPath = Path.GetFullPath(Path.Combine(currentDir, @"XML"));
                //var SerializePath = Path.GetFullPath(Path.Combine(XmlPath, $"ConverterXmlFile number {counter}.xml"));
               


                if (!(Directory.Exists(JsonPath) && Directory.Exists(XmlPath)))
                {
                    Directory.CreateDirectory(JsonPath);
                    Directory.CreateDirectory(XmlPath);

                }

                string[] jsonFiles = Directory.GetFiles(JsonPath, "*.json");
                if (jsonFiles != null)
                {
                    foreach (var instanceJson in jsonFiles)
                    {
                       counter+=1;
                        var SerializePath = Path.GetFullPath(Path.Combine(XmlPath, $"ConverterXmlFile number {counter}.xml"));
                        pathJsonFile = Path.GetFullPath(Path.Combine(JsonPath, instanceJson));
                        using (FileStream fs = new FileStream(pathJsonFile, FileMode.OpenOrCreate))
                        {
                            storageJsonFile = await JsonSerializer.DeserializeAsync<List<Person>>(fs);
                        }

                        XmlSerializer xmLserializer = new XmlSerializer(typeof(List<Person>));

                        using (FileStream fx = new FileStream(SerializePath, FileMode.OpenOrCreate))
                        {
                            xmLserializer.Serialize(fx, storageJsonFile);
                            Console.WriteLine("Объект сериализован");
                        }
                    }
                }
            }
            while (Exit != "q");
        }

    }
}
