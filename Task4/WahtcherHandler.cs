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
        private readonly ILoger loger;

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
            string defaultFolder = d.DefaultFolder.Path;


            foreach (TemplateElement item in d.Templates)
            {

                if (Regex.IsMatch(e.Name, item.NameTemplate))
                {
                    string destinationPath = CreateNewPath(e.FullPath, item);

                    MoveTo(e.FullPath, destinationPath);
                    loger.TemplateFound(true);
                    return;
                }

            }            

            loger.TemplateFound(false);
            MoveTo(e.FullPath, Path.Combine(defaultFolder,e.Name));
            
        }



        private string CreateNewPath(string sourceFilePath, TemplateElement template)
        {

            string fullPath;

            string fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            string extension = Path.GetExtension(sourceFilePath);

            if (template.IsAddCreationDate)
            {
                fileName = $"{fileName} ({DateTime.Now.ToLongDateString()})";
            }

            if (template.IsAddIndex)
            {
                int index = Directory.GetFiles(template.DestinationFolder).Length;
                fileName = $"{fileName} ({index + 1})";
            }


            fullPath = Path.Combine(template.DestinationFolder, $"{fileName}{extension}");

            return fullPath;

        }


        private void MoveTo(string sourceFilePath, string newFilePath)
        {
            const int maxNumberFailure = 50;

            bool fileLocked = true;
            int failureСounter = 0;

            while (fileLocked)
            {
                try
                {
                    if (File.Exists(newFilePath))
                    {
                        File.Delete(newFilePath);
                    }


                    if (File.Exists(sourceFilePath))
                    {
                        File.Move(sourceFilePath, newFilePath);

                        string fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
                        loger.FileMoved(fileName, newFilePath);
                    }

                    fileLocked = false;
                }
                catch (IOException ex)
                {
                    failureСounter++;

                    if (failureСounter == maxNumberFailure)
                    {
                        loger.Error(ex.Message);
                        fileLocked = false;
                    }

                }
               

            }

        }

    }
}
