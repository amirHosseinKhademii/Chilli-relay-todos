namespace hot_demo.mutations;

// [Authorize]
public partial class Mutation
{
    public async Task<Todo> CreateTodo([Service] Service service, [Service] ITopicEventSender sender, ClaimsPrincipal claimsPrincipal, string title,
        string? body)
    {
        var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

        return await service.CreateTodosAsync(title, userId, body);
        // await sender.SendAsync("TodoAdded", todo);
        // return todo;
    }
    public async Task<string> RemoveTodo([Service] Service service, string id) => await service.RemoveTodoAsync(id);

    public async Task<Todo> CompleteTodo([Service] Service service, string id, bool isCompleted) => await service.CompleteTodoAsync(id, isCompleted);

    public async Task<Todo> UpdateTodo([Service] Service service, string id, string title, string body) => await service.UpdateTodoAsync(id, title, body);
}
