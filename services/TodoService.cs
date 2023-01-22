
namespace hot_demo.services;

public partial class Service
{
    [UsePaging]
    public async Task<IExecutable<Todo>> GetTodosAsync() => _todosCollection.AsExecutable();

    public async Task<Todo> GetTodoByIdAsync([ID] string id) => await _todosCollection.Find(todo => todo.Id == id).FirstAsync();

    public async Task<Todo> CreateTodosAsync(string title, string userId, string? body)
    {

        var todo = new Todo()
        {
            Title = title,
            Author = userId,
            Body = body,
            CreatedDate = DateTime.Now,
            IsCompleted = false,
        };
        await _todosCollection.InsertOneAsync(todo);
        return todo;

    }

    public async Task<string> RemoveTodoAsync(string todoId)
    {
        await _todosCollection.DeleteOneAsync(todo => todo.Id == todoId);
        return todoId;
    }

    public async Task<Todo> CompleteTodoAsync(string id, bool isCompleted)
    {
        var update = Builders<Todo>.Update.Set(todo => todo.IsCompleted, isCompleted);
        var filter = Builders<Todo>.Filter.Where(item => item.Id == id);
        var todo = await _todosCollection.FindOneAndUpdateAsync(filter, update);
        todo.IsCompleted = isCompleted;
        return todo;

    }

    public async Task<Todo> UpdateTodoAsync(string id, string title, string body)
    {
        var updateBody = Builders<Todo>.Update.Set(todo => todo.Body, body);
        var updateTitle = Builders<Todo>.Update.Set(todo => todo.Title, title);
        var update = Builders<Todo>.Update.Combine(updateBody, updateTitle);
        var filter = Builders<Todo>.Filter.Where(item => item.Id == id);
        var todo = await _todosCollection.FindOneAndUpdateAsync(filter, update);
        todo.Title = title;
        todo.Body = body;
        return todo;
    }
}