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
            //List<string> folderPaths = new List<string>()
            //{
            //     @"C:\Users\iammr\Desktop\Task4\Task4\bin\Debug\New folder",
            //     @"C:\Users\iammr\Desktop\Task4\Task4\bin\Debug\New folder (2)",
            //     @"C:\Users\iammr\Desktop\Task4\Task4\bin\Debug\New folder (3)"
            //};


            

            var d = (CustomConfigurationSection)ConfigurationManager.GetSection("customSection");
            //d.Culture.Culture = "ru-RU";

            var folderPaths = d.Paths;
          

            //d.Culture.Culture = CultureInfo.GetCultureInfo("ru-Ru");
            Thread.CurrentThread.CurrentCulture = new CultureInfo(d.Culture.Culture);


            WahtcherHandler wahtcherHandler = new WahtcherHandler();

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
