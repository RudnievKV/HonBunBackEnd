namespace DictionaryApi.Models
{
    public class DBSettings : IDBSettings
    {
        public string ConnectionString { get; init; }
        public string DatabaseName { get; init; }
        public string WordsCollectionName { get; init; }
        public string NamesCollectionName { get; init; }
        public string KanjiCollectionName { get; init; }
    }
    public interface IDBSettings
    {
        string ConnectionString { get; init; }
        string DatabaseName { get; init; }
        string WordsCollectionName { get; init; }
        string NamesCollectionName { get; init; }
        string KanjiCollectionName { get; init; }
    }
}
