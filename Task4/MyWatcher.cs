using System.Collections.Generic;
using System.IO;

namespace Task4
{

    

    class MyWatcher
    {
        private List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();


        public MyWatcher(List<string> folderPaths, List<FileSystemEventHandler> handlers)
        {
            CreateWatchers(folderPaths,handlers);
        }

        /// <summary>
        /// Создает экземпляры <see cref="FileSystemWatcher"/> для каждой папки 
        /// </summary>
        /// <param name="folderPaths">Список путей к папкам для прослушивания</param>
        /// <param name="handlers">Список обработчиков</param>
        private void CreateWatchers(List<string> folderPaths, List<FileSystemEventHandler> handlers)
        {
            
            foreach (var folderPath in folderPaths)
            {
                FileSystemWatcher watcher = new FileSystemWatcher(folderPath);


                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

                if (handlers!=null)
                {
                    foreach (var item in handlers)
                    {
                        watcher.Created += item;
                    }
                }                               

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
