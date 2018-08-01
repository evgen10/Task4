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
            throw new NotImplementedException();
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
    }
}
