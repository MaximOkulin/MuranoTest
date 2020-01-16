using SearchEngine.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine.Models.Settings
{
    public class Settings : ISettings
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string RequestFormat { get; set; }
    }
}
