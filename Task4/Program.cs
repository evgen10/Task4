using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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
                        

            var configuration = (CustomConfigurationSection)ConfigurationManager.GetSection("customSection");
   

            var folderPaths = configuration.Paths;
          
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo(configuration.Culture.Culture);


            WahtcherHandler wahtcherHandler = new WahtcherHandler(new ConsoleLoger());

            List<FileSystemEventHandler> handlers = new List<FileSystemEventHandler>();
            handlers.Add(wahtcherHandler.OnChanged);
            handlers.Add(wahtcherHandler.MoveFile);


            MyWatcher wathcer = new MyWatcher(folderPaths, handlers);                     

         
            while (true)
            {

            }

        }


    }
}
