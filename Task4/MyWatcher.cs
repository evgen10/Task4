using System.Collections.Generic;
using System.IO;

using Task4.Configuration;

namespace Task4
{

    

    class MyWatcher
    {
        private List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();


        public MyWatcher(ListenedFolderPathElementCollection folderPaths, List<FileSystemEventHandler> handlers)
        {
            CreateWatchers(folderPaths,handlers);
        }

        /// <summary>
        /// Создает экземпляры <see cref="FileSystemWatcher"/> для указаных папок
        /// </summary>
        /// <param name="folderPaths">Список путей к папкам для прослушивания</param>
        /// <param name="handlers">Список обработчиков</param>
        private void CreateWatchers(ListenedFolderPathElementCollection folderPaths, List<FileSystemEventHandler> handlers)
        {
            
            foreach (ListenedFolderPathElement folderPath in folderPaths)
            {
                FileSystemWatcher watcher = new FileSystemWatcher(folderPath.FolderPath);

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
    }
}
