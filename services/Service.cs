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

        var mongoConnectionUrl = new MongoUrl(settings.Value.LocalConnectionString);
        var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);

        var mongoClient = new MongoClient(mongoClientSettings);
        var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<User>("Users");
        _booksCollection = mongoDatabase.GetCollection<Book>("Books");
        _todosCollection = mongoDatabase.GetCollection<Todo>("Todos");
    }
}