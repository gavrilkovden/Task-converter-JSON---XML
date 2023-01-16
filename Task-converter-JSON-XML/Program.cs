using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Task_converter_JSON_XML
{
    class Program
    {
        static string currentDir = Directory.GetCurrentDirectory();
        static string jsonPath = Path.GetFullPath(Path.Combine(currentDir, @"JSON"));
        static string xmlPath = Path.GetFullPath(Path.Combine(currentDir, @"XML"));
        static string directoryInvalidJsonPath = Path.GetFullPath(Path.Combine(currentDir, @"InvalidJson"));
        static int counter = 0;
        static string instanceJson;
        static List<Person> storagePeopleFromJson = new List<Person>();
        static string Exit = null;
        static string pathJsonFile;
        static string serializePath;

        static async Task Main(string[] args)
        {
            do
            {
                try
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

                            storagePeopleFromJson = await ReadFromJson<List<Person>>(pathJsonFile);

                            if (storagePeopleFromJson.Any())
                            {
                                WriteToXml(storagePeopleFromJson, serializePath);
                                File.Delete(instanceJson);
                            }
                        }
                    }
                }
                catch (JsonException ex)
                {
                    Log(ex.ToString());
                    CreateIfNotExists(directoryInvalidJsonPath);
                    string fileInvalidJsonPath = Path.GetFullPath(Path.Combine(directoryInvalidJsonPath, $"invalid jsonFile number{counter}"));
                    File.Move(pathJsonFile, fileInvalidJsonPath);
                }

                catch (Exception ex)
                {
                    Log(ex.ToString());
                    CreateIfNotExists(directoryInvalidJsonPath);
                    string fileInvalidJsonPath = Path.GetFullPath(Path.Combine(directoryInvalidJsonPath, $"The invalid File {counter}"));
                    File.Move(pathJsonFile, fileInvalidJsonPath);
                }

                finally
                {
                    storagePeopleFromJson.Clear();
                }
            }
            while (Exit != "q");
        }


        public static void Log(string message)
        {
            File.AppendAllText("log.log", message);
        }


        public static async Task<T> ReadFromJson<T>(string pathJson)
        {
            T result;
            using (FileStream fs = new FileStream(pathJson, FileMode.Open))
            {
                result = await JsonSerializer.DeserializeAsync<T>(fs);
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
