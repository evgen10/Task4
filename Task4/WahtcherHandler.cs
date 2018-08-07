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

        public void OnFileFounded(object source, FileSystemEventArgs e)
        {
            loger.PrintFileFound(e.Name);
        }

        public void MoveFile(object source, FileSystemEventArgs e)
        {
            var config = (CustomConfigurationSection)ConfigurationManager.GetSection("customSection");

            string defaultFolder = config.DefaultFolder.Path;
            string destinationPath;


            foreach (TemplateElement item in config.Templates)
            {
                if (Regex.IsMatch(e.Name, item.NameTemplate))
                {
                    destinationPath = CreateNewPath(e.FullPath, item.DestinationFolder, item.IsAddCreationDate, item.IsAddIndex);

                    MoveTo(e.FullPath, destinationPath);
                    loger.PrintTemplateFound(true);

                    return;
                }

            }

            destinationPath = CreateNewPath(e.FullPath, defaultFolder, true, true);

            MoveTo(e.FullPath, destinationPath);
            loger.PrintTemplateFound(false);

        }
           
        private string CreateNewPath(string sourceFilePath, string destinationFolder, bool isAddDate, bool isAddIndex)
        {

            int index=0;

            string fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            string extension = Path.GetExtension(sourceFilePath);
            string fullPath = Path.Combine(destinationFolder,Path.GetFileName(sourceFilePath));

            string newFileName="";

            if (isAddDate)
            {
                fileName = $"{fileName} ({DateTime.Now.ToLongDateString()})";
            }
            
            while (File.Exists(fullPath))
            {              
                if (isAddIndex)
                {
                    newFileName = $"{fileName} ({index + 1})";
                }
                
                fullPath = Path.Combine(destinationFolder, $"{newFileName}{extension}");
                index++;
            }

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
                        loger.PrintFileMoved(fileName, newFilePath);
                    }

                    fileLocked = false;
                }
                catch (IOException ex)
                {
                    failureСounter++;

                    if (failureСounter == maxNumberFailure)
                    {
                        loger.PrintError(ex.Message);
                        fileLocked = false;
                    }

                }

            }

        }

    }
}
