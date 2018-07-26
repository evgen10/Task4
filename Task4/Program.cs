using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> folderPaths = new List<string>()
            {
                 @"C:\Users\Evgeniy_Chernyshkov\Desktop\New folder",
                 @"C:\Users\Evgeniy_Chernyshkov\Desktop\New folder (3)",
                 @"C:\Users\Evgeniy_Chernyshkov\Desktop\New folder (4)"
            };


            MyWatcher wathcer = new MyWatcher(folderPaths);


            var wathcers = wathcer.GetWatchers(folderPaths);

         
            while (true)
            {

            }

        }

    }



    public static class WahtcherHandler
    {
        public static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }
    }

    class MyWatcher
    {
        private List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();


        public MyWatcher(List<string> folderPaths)
        {
            CreateWatchers(folderPaths);
        }

        private void CreateWatchers(List<string> folderPaths)
        {

            foreach (var folderPath in folderPaths)
            {
                FileSystemWatcher watcher = new FileSystemWatcher(folderPath);


                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

                watcher.Changed += WahtcherHandler.OnChanged;
                watcher.Created += WahtcherHandler.OnChanged;
                watcher.Deleted += WahtcherHandler.OnChanged;
                watcher.Renamed += WahtcherHandler.OnChanged;

                watcher.EnableRaisingEvents = true;

                watchers.Add(watcher);

            }
        }

        public List<FileSystemWatcher> GetWatchers(List<string> folderPaths)
        {
            return watchers;
        }


    }
}
