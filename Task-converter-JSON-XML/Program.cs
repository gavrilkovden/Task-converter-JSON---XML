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
        static string currentDir = Directory.GetCurrentDirectory();
        static string jsonPath = Path.GetFullPath(Path.Combine(currentDir, @"JSON"));
        static string xmlPath = Path.GetFullPath(Path.Combine(currentDir, @"XML"));
        static string directoryInvalidJsonPath = Path.GetFullPath(Path.Combine(currentDir, @"InvalidJson"));
        public static string pathJsonFile;
        static int counter = 0;
        static string SerializePath;
        static string instanceJson;
        static List<Person> storagePeopleFromJson = new List<Person>();
        static string Exit = null;

        static async Task Main(string[] args)
        {
            do
            {
                try
                {
                    if (!(Directory.Exists(jsonPath))) { Directory.CreateDirectory(jsonPath); }
                    if (!(Directory.Exists(xmlPath))) { Directory.CreateDirectory(xmlPath); }

                    string[] jsonFiles = Directory.GetFiles(jsonPath);
                    if (jsonFiles.Count() != 0)
                    {
                        foreach (var instJson in jsonFiles)
                        {
                            instanceJson = instJson;
                            storagePeopleFromJson = await ReadFromJson<List<Person>>();

                            if (storagePeopleFromJson.Any())
                            {
                                WriteToXml(storagePeopleFromJson);
                                File.Delete(instanceJson);
                            }
                        }
                    }
                }
                catch (JsonException ex)
                {
                    Log(ex.ToString());
                    if (!(Directory.Exists(directoryInvalidJsonPath))) { Directory.CreateDirectory(directoryInvalidJsonPath); }
                    string fileInvalidJsonPath = Path.GetFullPath(Path.Combine(directoryInvalidJsonPath, $"invalid jsonFile number{counter}"));
                    File.Move(pathJsonFile, fileInvalidJsonPath);
                }

                catch (Exception ex)
                {
                    Log(ex.ToString());
                    if (!(Directory.Exists(directoryInvalidJsonPath))) { Directory.CreateDirectory(directoryInvalidJsonPath); }
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


        public static async Task<T> ReadFromJson<T>()
        {
            T result;
            counter += 1;
            SerializePath = Path.GetFullPath(Path.Combine(xmlPath, $"ConverterXmlFile number {counter}.xml"));
            pathJsonFile = Path.GetFullPath(Path.Combine(jsonPath, instanceJson));
            using (FileStream fs = new FileStream(pathJsonFile, FileMode.Open))
            {
                result = await JsonSerializer.DeserializeAsync<T>(fs);
            }
            return result;
        }


        public static void WriteToXml(List<Person> people)
        {
            XmlSerializer xmLserializer = new XmlSerializer(typeof(List<Person>));

            using (FileStream fx = new FileStream(SerializePath, FileMode.Create))
            {
                xmLserializer.Serialize(fx, people);
                Console.WriteLine($"Объект {instanceJson} сериализован");
            }
        }
    }
}
