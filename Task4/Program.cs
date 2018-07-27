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
                 @"C:\Users\iammr\Desktop\Task4\Task4\bin\Debug\New folder",
                 @"C:\Users\iammr\Desktop\Task4\Task4\bin\Debug\New folder (2)",
                 @"C:\Users\iammr\Desktop\Task4\Task4\bin\Debug\New folder (3)"
            };
            

            List<FileSystemEventHandler> handlers = new List<FileSystemEventHandler>();
            handlers.Add(WahtcherHandler.OnChanged);
            handlers.Add(WahtcherHandler.MoveFile);


            MyWatcher wathcer = new MyWatcher(folderPaths,handlers);


            var wathcers = wathcer.GetWatchers(folderPaths);

         
            while (true)
            {

            }

        }


    }
}
