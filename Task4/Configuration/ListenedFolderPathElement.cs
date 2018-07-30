using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Task4.Configuration
{
    public class ListenedFolderPathElement : ConfigurationElement
    {
        [ConfigurationProperty("path",IsKey =true)]
        public string FolderPath
        {
            get { return (string)this["path"]; }
        }

    }
}
