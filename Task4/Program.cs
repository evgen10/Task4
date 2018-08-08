using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System;
using Task4.Configuration;
using System.Configuration;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Task4
{
    class Program
    {
       
        private static ListenedFolderPathElementCollection listenedFolders;


        static void Main(string[] args)
        {
            //инициализируем настройки при старте приложения для локализации меню
            Initialize();
            
            Menu();

            //инициализируем настройки после изменений сделанных в меню
            Initialize();

            ConsoleLoger loger = new ConsoleLoger();
            WahtcherHandler wahtcherHandler = new WahtcherHandler(loger);
            

            List<FileSystemEventHandler> handlers = new List<FileSystemEventHandler>
            {
                wahtcherHandler.OnFileFound,
                wahtcherHandler.MoveFile
            };

            MyWatcher wathcer = new MyWatcher(listenedFolders, handlers, loger);

            ShowListeningFolders();    

            while (true)
            {
                
            }

        }



        private static void Initialize()
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var configuration = (CustomConfigurationSection)configFile.Sections["customSection"];

            CultureInfo culture = new CultureInfo(configuration.Culture.Culture);


            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            CreateDirectory(configuration.Paths);

            listenedFolders = configuration.Paths;


        }

        private static void CreateDirectory(ListenedFolderPathElementCollection folders)
        {
            foreach (ListenedFolderPathElement folder in folders)
            {
                if (!Directory.Exists(folder.FolderPath))
                {
                    Directory.CreateDirectory(folder.FolderPath);
                }
            }
        }
        
        private static void SetLanguage()
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var configuration = (CustomConfigurationSection)configFile.Sections["customSection"];   

            configuration.Culture.Culture = LanguageMenu();

            configFile.AppSettings.SectionInformation.ForceSave = true;
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }        


        private static string LanguageMenu()
        {
            while (true)
            {
                Console.WriteLine($"{Resources.Menu.Press} 1 {Resources.Menu.ChooseRussian}");
                Console.WriteLine($"{Resources.Menu.Press} 2 {Resources.Menu.ChooseEnglish}");

                string key = Console.ReadLine();

                if (key == "1")
                {
                    return "ru-RU";
                }
                if (key == "2")
                {
                    return "en-US";
                }

                Console.WriteLine(Resources.Menu.InvalidInput);
            }


        }

        private static void Menu()
        {
            while (true)
            {
                Console.WriteLine($"{Resources.Menu.Press} 1 {Resources.Menu.ChangeLanguage}");
                Console.WriteLine($"{Resources.Menu.Press} 2 {Resources.Menu.Continue}");

                string key = Console.ReadLine();

                if (key == "1")
                {
                    SetLanguage();
                    return;
                }
                if (key == "2")
                {
                    return;
                }

                Console.WriteLine(Resources.Menu.InvalidInput);

            }

        }

        private static void ShowListeningFolders()
        {
            Console.Clear();
            Console.WriteLine($"{Resources.Menu.ListeningStarted}.");
            Console.WriteLine($"{Resources.Menu.FoldersForListening}:");

            foreach (ListenedFolderPathElement item in listenedFolders)
            {
                Console.WriteLine(item.FolderPath);              
            }
            
        }

    }
}
