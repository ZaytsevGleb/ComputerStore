namespace DataAccess.Options
{
    public sealed class MongoDbOptions
    {
        public const string SeciotnName = "MongoDB";
        public string ConnectionURI { get; set; } = default!;
        public string DatabaseName { get; set; } = default!;
        public string CollectionName { get; set; } = default!;
    }
}
