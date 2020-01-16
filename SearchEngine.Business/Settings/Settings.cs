using SearchEngine.Business.Interfaces;

namespace SearchEngine.Business.Settings
{
    public class Settings : ISettings
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string RequestFormat { get; set; }
    }
}
