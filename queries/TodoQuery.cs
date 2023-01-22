namespace hot_demo.queries;

public partial class Query
{
    // [Authorize]
    // public async Task<List<Todo>> GetTodos([Service] Service service, ClaimsPrincipal claimsPrincipal) =>
    //     await service.GetTodosAsync(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));

    [UsePaging]
    public Task<IExecutable<Todo>> GetTodos(
         [Service] Service service)
         => service.GetTodosAsync();
}