using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Task_converter_JSON_XML
{
    [Serializable]
    public class JsnoToXmlConverter
    {
        static public string currentDir = Directory.GetCurrentDirectory();
        static string jsonPath = Path.GetFullPath(Path.Combine(currentDir, @"JSON"));
        static string xmlPath = Path.GetFullPath(Path.Combine(currentDir, @"XML"));
        static public int counter = 0;
        static string instanceJson;
        static public List<Person> storagePeopleFromJson = new List<Person>();
        static public string pathJsonFile;
        static string serializePath;

        static public async Task Convert()
        {
            CreateIfNotExists(jsonPath);
            CreateIfNotExists(xmlPath);
            string[] jsonFiles = Directory.GetFiles(jsonPath);
            if (jsonFiles.Count() != 0)
            {
                foreach (var instJson in jsonFiles)
                {
                    counter += 1;
                    instanceJson = instJson;
                    serializePath = Path.GetFullPath(Path.Combine(xmlPath, $"ConverterXmlFile number {counter}.xml"));
                    pathJsonFile = Path.GetFullPath(Path.Combine(jsonPath, instanceJson));

                    storagePeopleFromJson = await ReadFromJson(pathJsonFile);

                    if (storagePeopleFromJson.Any())
                    {
                        WriteToXml(storagePeopleFromJson, serializePath);
                        File.Delete(instanceJson);
                    }
                }
            }
        }

        public static async Task<List<Person>> ReadFromJson(string pathJson)
        {
            List<Person> result;
            using (FileStream fs = new FileStream(pathJson, FileMode.Open))
            {
                result = await JsonSerializer.DeserializeAsync<List<Person>>(fs);
            }
            return result;
        }


        public static void WriteToXml(List<Person> people, string serialPath)
        {
            XmlSerializer xmLserializer = new XmlSerializer(typeof(List<Person>));
            using (FileStream fx = new FileStream(serialPath, FileMode.Create))
            {
                xmLserializer.Serialize(fx, people);
                Console.WriteLine($"Объект {instanceJson} сериализован");
            }
        }

        public static void CreateIfNotExists(string path)
        {
            if (!(Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
