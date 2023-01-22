namespace hot_demo.types
{
    public record User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }

        // public async Task<List<Todo>> GetTodos([Service] Service service, [Parent] User user) => await service.GetTodosAsync(user.Id);
    }
}