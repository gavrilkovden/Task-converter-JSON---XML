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
        static string directoryInvalidJsonPath = Path.GetFullPath(Path.Combine(JsnoToXmlConverter.currentDir, @"InvalidJson"));
        static string Exit = null;

        static async Task Main(string[] args)
        {
            do
            {
                try
                {
                    await JsnoToXmlConverter.Convert();
                }

                catch (JsonException ex)
                {
                    Log(ex.ToString());
                    JsnoToXmlConverter.CreateIfNotExists(directoryInvalidJsonPath);
                    string fileInvalidJsonPath = Path.GetFullPath(Path.Combine(directoryInvalidJsonPath, $"invalid jsonFile number{JsnoToXmlConverter.counter}"));
                    File.Move(JsnoToXmlConverter.pathJsonFile, fileInvalidJsonPath);
                }

                catch (Exception ex)
                {
                    Log(ex.ToString());
                    JsnoToXmlConverter.CreateIfNotExists(directoryInvalidJsonPath);
                    string fileInvalidJsonPath = Path.GetFullPath(Path.Combine(directoryInvalidJsonPath, $"The invalid File {JsnoToXmlConverter.counter}"));
                    File.Move(JsnoToXmlConverter.pathJsonFile, fileInvalidJsonPath);
                }

                finally
                {
                    JsnoToXmlConverter.storagePeopleFromJson.Clear();
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
