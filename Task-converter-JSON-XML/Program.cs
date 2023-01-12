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
        public static string pathJsonFile;


        static async Task Main(string[] args)
        {
            int counter = 0;
            string Exit = null;
            List<Person> storageJsonFile = new List<Person>();
            string SerializePath = null;
            do
            {
                //    Console.WriteLine("The program is running. To terminate the program enter q");
                //    Exit = Console.ReadLine();
                if (!(Directory.Exists(jsonPath) && Directory.Exists(xmlPath)))
                {
                    Directory.CreateDirectory(jsonPath);
                    Directory.CreateDirectory(xmlPath);

                }

                string[] jsonFiles = Directory.GetFiles(jsonPath, "*.json");
                if (jsonFiles != null)
                {
                    foreach (var instanceJson in jsonFiles)
                    {
                        try
                        {
                            counter += 1;
                            SerializePath = Path.GetFullPath(Path.Combine(xmlPath, $"ConverterXmlFile number {counter}.xml"));
                            pathJsonFile = Path.GetFullPath(Path.Combine(jsonPath, instanceJson));
                            using (FileStream fs = new FileStream(pathJsonFile, FileMode.Open))
                            {
                                storageJsonFile = await JsonSerializer.DeserializeAsync<List<Person>>(fs);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log($"The file {instanceJson} could not deserialize. Check if the file corresponds to the class of Person");
                            Log(ex.Message);
                            File.Delete(pathJsonFile);
                            storageJsonFile.Clear();
                        }
                        finally
                        {
                            if (storageJsonFile.Any())
                            {
                                try
                                {
                                    XmlSerializer xmLserializer = new XmlSerializer(typeof(List<Person>));

                                    using (FileStream fx = new FileStream(SerializePath, FileMode.Create))
                                    {
                                        xmLserializer.Serialize(fx, storageJsonFile);
                                        Console.WriteLine($"Объект {instanceJson} сериализован");
                                    }
                                    File.Delete(pathJsonFile);
                                }
                                catch (Exception ex)
                                {
                                    Log("The file could not serialize. Check if the file corresponds to the class of Person");
                                    Log(ex.Message);
                                    File.Delete(pathJsonFile);
                                }
                            }
                        }
                    }
                }
            }
            while (Exit != "q");
        }

        public static void Log(string message)
        {
            File.AppendAllText("log.log", message);
        }

    }

}
