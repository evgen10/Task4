using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System;
using Task4.Configuration;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace Task4
{
    class Program
    {       

        static void Main(string[] args)
        {

            Console.WriteLine("Press 1 to choose language");
            Console.WriteLine("Press any to continue.");

            int key = Console.Read();

            if (key == 49 )
            {

            }



            ConsoleLoger loger = new ConsoleLoger();
            WahtcherHandler wahtcherHandler = new WahtcherHandler(loger);


            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var configuration = (CustomConfigurationSection)configFile.Sections["customSection"];

            configuration.Culture.Culture = "ru-RU";


            configFile.AppSettings.SectionInformation.ForceSave = true;
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);


            var folderPaths = configuration.Paths;


            Thread.CurrentThread.CurrentCulture = new CultureInfo(configuration.Culture.Culture);


            List<FileSystemEventHandler> handlers = new List<FileSystemEventHandler>();
            handlers.Add(wahtcherHandler.OnChanged);
            handlers.Add(wahtcherHandler.MoveFile);


            MyWatcher wathcer = new MyWatcher(folderPaths, handlers, loger);


            while (true)
            {

            }

        }





        private static void SetLanguage()
        {

            Console.WriteLine("Press 1 to choose language");
            Console.WriteLine("Press any to continue.");
                        

        }

    }
}
