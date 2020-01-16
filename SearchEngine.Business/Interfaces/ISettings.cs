namespace SearchEngine.Business.Interfaces
{
    public interface ISettings
    {
        string Name { get; set; }
        string Value { get; set; }
        string RequestFormat { get; set; }
    }
}
