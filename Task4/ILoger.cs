using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public interface ILoger
    {
        void FileFound(string fullPath, string name);
        void FileMoved(string fileName, string directoryName);
        void TemplateFound(bool isFound);
        void Error(string message);

    }
}
