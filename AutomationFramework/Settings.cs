using AutomationFramework.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework
{
    class Settings
    {
        public static BrowserType BrowserType
        {
            get
            {
                return (BrowserType)Enum.Parse(typeof(BrowserType), ConfigurationManager.AppSettings["BrowserType"]);
            }
        }
    }
}
