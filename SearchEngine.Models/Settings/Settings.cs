using SearchEngine.Models.Interfaces;

namespace SearchEngine.Models.Settings
{
    public class Settings : ISettings
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string RequestFormat { get; set; }
    }
}
