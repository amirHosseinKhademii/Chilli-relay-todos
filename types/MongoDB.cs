namespace hot_demo.types
{
    public class MongoDBSetting
    {
        public string ConnectionString { get; init; } = null!;

        public string DatabaseName { get; init; } = null!;

        public string LocalConnectionString { get; init; } = null!;
    }
}