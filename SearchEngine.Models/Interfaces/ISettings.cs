namespace SearchEngine.Models.Interfaces
{
    public interface ISettings
    {
        string Name { get; set; }
        string Value { get; set; }
        string RequestFormat { get; set; }
    }
}
