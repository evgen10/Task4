using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace Task4.Configuration
{
   public class TemplateElement: ConfigurationElement
    {

        [ConfigurationProperty("template",IsKey =true)]
        public string NameTemplate
        {
            get { return (string)this["template"]; }
        }


        [ConfigurationProperty("destinationFolder")]
        public string DestinationFolder
        {
            get { return (string)this["destinationFolder"]; }
        }

        [ConfigurationProperty("addCreationDate")]
        public bool IsAddCreationDate
        {
            get{ return (bool)this["addCreationDate"];}
        }

        [ConfigurationProperty("addIndex")]
        public bool IsAddIndex
        {
            get { return (bool)this["addIndex"]; }
        }

    }
}
