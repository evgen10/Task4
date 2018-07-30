using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace Task4.Configuration
{
    public class CustomConfigurationSection: ConfigurationSection
    {
        [ConfigurationProperty("culture")]
        public CultureElement Culture
        {
            get { return (CultureElement)this["culture"]; }
            set { this["culture"] = value; }
        }


        [ConfigurationProperty("paths")]
        public ListenedFolderPathElementCollection Paths
        {
            get { return (ListenedFolderPathElementCollection)this["paths"]; }
        }

        [ConfigurationProperty("templates")]
        public TemplateElementCollection Templates
        {
            get { return (TemplateElementCollection)this["templates"]; }
        }

    }
}
