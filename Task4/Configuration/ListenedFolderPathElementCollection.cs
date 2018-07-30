using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Task4.Configuration
{
    public class ListenedFolderPathElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ListenedFolderPathElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ListenedFolderPathElement)element).FolderPath;
        }
    }
}
