
namespace hot_demo.types;

[Node]
public record Todo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [ID]
    public string Id { get; init; }

    public string Title { get; set; }

    public string? Body { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedDate { get; init; }

    [GraphQLIgnore]
    public string? Author { get; init; }

    // public async Task<User> GetUser([Service] Service service, [Parent] Todo todo) => await service.GetUserAsync(todo.Author);
    public static async Task<Todo> Get([ID] string id,
        [Service] Service service)
    {
        return await service.GetTodoByIdAsync(id);

    }
}
