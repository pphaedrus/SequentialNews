namespace NewsCase.Business.Settings
{
    public interface IDatabaseSettings
    {
        public string NewsCollectionName { get; set; }
        public string SequentialNewsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}