using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Task_converter_JSON_XML
{
    public static class DirectoryExtension
    {
       public static void CreateIfNotExists( string path)
        {
            if (!(Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
