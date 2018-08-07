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

        
        public void OnFileFound(object source, FileSystemEventArgs e)
        {
            loger.PrintFileFound(e.Name);
        }

        public void MoveFile(object source, FileSystemEventArgs e)
        {
            
            var config = (CustomConfigurationSection)ConfigurationManager.GetSection("customSection");

            string defaultFolder = config.DefaultFolder.Path;
            string destinationPath;

            //проходим по всем шаблонам для определения папки для перемещения файла 
            foreach (TemplateElement item in config.Templates)
            {
                //если имя файла соответсвует регулярному выражению
                if (Regex.IsMatch(e.Name, item.NameTemplate))
                {
                    //получаем путь к файлу с изменённым именем 
                    destinationPath = CreateNewPath(e.FullPath, item.DestinationFolder, item.IsAddCreationDate, item.IsAddIndex);

                    //перемещаем появившийся  в прослушиваемой папке файл в назначенную шаблонам папку
                    MoveTo(e.FullPath, destinationPath);

                    //уведомляем о том, что шаблон найден
                    loger.PrintTemplateFound(true);

                    return;
                }

            }

            //если подходящего шаблона не найдено
            //создаем новый путь с папкой по умолчанию
            destinationPath = CreateNewPath(e.FullPath, defaultFolder, true, true);

            MoveTo(e.FullPath, destinationPath);
            //уведомляем о том, что шаблон не найден
            loger.PrintTemplateFound(false);

        }

        //создает новый путь к файлу с изменённым именем
        private string CreateNewPath(string sourceFilePath, string destinationFolder, bool isAddDate, bool isAddIndex)
        {
            //хранит порядковый номер файла в папке
            int index = 0;

            string fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            string extension = Path.GetExtension(sourceFilePath);
            string fullPath = Path.Combine(destinationFolder, Path.GetFileName(sourceFilePath));

            string newFileName = "";

            if (isAddDate)
            {
                fileName = $"{fileName} ({DateTime.Now.ToLongDateString()})";
                fullPath = Path.Combine(destinationFolder, $"{fileName}{extension}");
            }

            //цикл работает, пока не будет подобрано уникальное имя для файла
            while (File.Exists(fullPath))
            {
                newFileName = $"{fileName} ({index + 1})";

                fullPath = Path.Combine(destinationFolder, $"{newFileName}{extension}");
                index++;
            }            

            return fullPath;
        }

        //метод перемещает указанный файл в назначенное место
        private void MoveTo(string sourceFilePath, string newFilePath)
        {
            //максимальное количество попыток обращения к файлу
            const int maxNumberFailure = 50;

            //флаг для определения свободен ли файл от других процессов
            bool fileLocked = true;

            //счетчик неудачных попыток получить доступ к файлу
            int failureСounter = 0;

            //пока файл занят
            while (fileLocked)
            {
                try
                {
                    //если файл уже существует в  назначенной папке, то удаляем его
                    if (File.Exists(newFilePath))
                    {
                        File.Delete(newFilePath);
                    }

                    //если файл который необходимо переместить существует
                    if (File.Exists(sourceFilePath))
                    {
                        File.Move(sourceFilePath, newFilePath);

                        //получаем имя файла и записываем в журнал, что файл был перемещен 
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
