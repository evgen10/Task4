using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task4.Resources;

namespace Task4
{
    public class ConsoleLoger : ILoger
    {        

        public void PrintFileFound(string name)
        {
            Console.WriteLine();
            Console.WriteLine($"{Log.FoundFile} {name} ");
            Console.WriteLine($"{Log.CreationDate} - {DateTime.Now.ToLongDateString()}");
        }

        public void PrintFileMoved(string fileName, string directoryName)
        {
            Console.WriteLine($"{fileName} {Log.Moved} {directoryName}");
        }

        public void PrintTemplateFound(bool isFound)
        {
            if (isFound)
            {
                Console.WriteLine(Log.TemplateFound);

            }
            else
            {
                Console.WriteLine(Log.TemplateNotFound);
            }
        }

        public void PrintError(string message)
        {
            Console.WriteLine(message);
        }



    }
}
