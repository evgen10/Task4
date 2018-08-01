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
    public class WahtcherHandler
    {

        private ILoger loger;

        public WahtcherHandler(ILoger loger)
        {
            this.loger = loger;
        }

        public void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath);
            Console.WriteLine(e.Name);
            Console.WriteLine(DateTime.Now);
        }

        public void MoveFile(object source, FileSystemEventArgs e)
        {
            var d = (CustomConfigurationSection)ConfigurationManager.GetSection("customSection");
            string defaultFolder = @"C:\Users\iammr\Desktop\Task4\Task4\bin\Debug\Папка";


            foreach (TemplateElement item in d.Templates)
            {
                string destinationFolderPath = item.DestinationFolder;

                if (Regex.IsMatch(e.Name, item.NameTemplate))
                {
                    MoveTo(e.FullPath, destinationFolderPath);
                    loger.TemplateFound(true);
                    return;
                }

            }

            loger.TemplateFound(false);
            MoveTo(e.FullPath, defaultFolder);


        }


        private void MoveTo(string sourceFilePath, string newFilePath)
        {

            bool fileLocked = true;
            int failureСounter = 0;
            while (fileLocked)
            {
                try
                {

                    string fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
                    string extension = Path.GetExtension(sourceFilePath);

                    int p = Directory.GetFiles(newFilePath).Length;

                    string fullPath = Path.Combine(newFilePath, fileName + " (" + (p + 1) + ")" + extension);



                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }


                    if (File.Exists(sourceFilePath))
                    {
                        File.Move(sourceFilePath, fullPath);

                    }

                    fileLocked = false;
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                    failureСounter++;

                }

            }

        }

    }
}
