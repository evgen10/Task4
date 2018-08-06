using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public interface ILoger
    {
        void PrintFileFound(string name);
        void PrintFileMoved(string fileName, string directoryName);
        void PrintTemplateFound(bool isFound);
        void PrintError(string message);

    }
}
