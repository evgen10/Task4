using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using Task4.Configuration;

namespace Task4
{
    public  class WahtcherHandler
    {
        public  void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath);
            Console.WriteLine(e.Name);
            Console.WriteLine(DateTime.Now);
        }

        public  void MoveFile(object source, FileSystemEventArgs e)
        {
            var d = (CustomConfigurationSection)ConfigurationManager.GetSection("customSection");

            foreach (TemplateElement item in d.Templates)
            {
                string destinationFolderPath = Path.Combine(item.DestinationFolder, e.Name);
                
                if (Regex.IsMatch(e.Name, item.NameTemplate))
                {
                                      
                        if (File.Exists(destinationFolderPath))
                        {
                            File.Delete(destinationFolderPath);
                        }

                        File.Move(e.FullPath, destinationFolderPath + DateTime.Now.ToLongDateString());
               
                                      
                 
                    //File.Delete(e.FullPath);
                    return;

                }

            }

            Console.WriteLine("Шаблон не найден");




        }
    }
}
