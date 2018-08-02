using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public class ConsoleLoger : ILoger
    {
        public void FileFound(string fullPath, string name)
        {
            throw new NotImplementedException();
        }

        public void FileMoved(string fileName, string directoryName)
        {
            Console.WriteLine($"{fileName} moved to {directoryName}");
        }

        public void TemplateFound(bool isFound)
        {
            if (isFound)
            {
                Console.WriteLine("Шаблон найден");
            }
            else
            {
                Console.WriteLine("Шаблон не найден");
            }
        }


        public void Error(string message)
        {
            Console.WriteLine(message);
        }
    }
}
