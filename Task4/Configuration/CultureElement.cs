using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Globalization;

namespace Task4.Configuration
{
    public class CultureElement : ConfigurationElement
    {
        [ConfigurationProperty("cultureProp")]

        public string Culture
        {
            get { return (string)this["cultureProp"]; }
            set { this["cultureProp"] = value; }
        }

    }
}
