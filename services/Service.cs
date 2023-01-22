using MongoDB.Driver.Core.Events;

namespace hot_demo.services;

public partial class Service
{
    private readonly IJwtAuthentication _jwtAuthentication;
    private readonly IMongoCollection<User> _userCollection;
    private readonly IMongoCollection<Book> _booksCollection;
    private readonly IMongoCollection<Todo> _todosCollection;

    public Service(
        IOptions<MongoDBSetting> settings, IJwtAuthentication JwtAuthentication)
    {
        _jwtAuthentication = JwtAuthentication;

        var connectionString = settings.Value.ConnectionString;
        var mongoConnectionUrl = new MongoUrl("mongodb://localhost:27017");
        var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
        mongoClientSettings.ClusterConfigurator = cb =>
        {
            // This will print the executed command to the console
            cb.Subscribe<CommandStartedEvent>(e =>
            {
                Console.WriteLine($"{e.CommandName} - {e.Command.ToJson()}");
            });
        };
        var mongoClient = new MongoClient(mongoClientSettings);
        var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);


        // var mongoClient = new MongoClient(
        //     settings.Value.ConnectionString);

        // var mongoDatabase = mongoClient.GetDatabase(
        //     settings.Value.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<User>("Users");
        _booksCollection = mongoDatabase.GetCollection<Book>("Books");
        _todosCollection = mongoDatabase.GetCollection<Todo>("Todos");
    }
}