namespace AsyncCorrelationLogging.RestApi.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using System.Web.Http;
  using Domain;
  using LogService;
  using Models;

  public class TodoController : ApiController
  {
    private readonly Logger logger;
    private readonly TodoService todoService;

    public TodoController(Logger logger, TodoService todoService)
    {
      this.logger = logger;
      this.todoService = todoService;
    }

    // GET: api/Todo
    public async Task<IEnumerable<Todo>> Get()
    {
      return await this.todoService.GetAllAsync();
    }

    // GET: api/Todo/5
    public async Task<Todo> Get(Guid id)
    {
      return await this.todoService.GetByIdAsync(id);
    }

    /// <summary>
    /// The post.
    /// </summary>
    /// <param name="todo">
    /// The todo.
    /// </param>
    /// <returns>
    /// The <see cref="Task"/>.
    /// </returns>
    public async Task Post(Todo todo)
    {
      await this.todoService.AddAsync(todo);
    }

    // PUT: api/Todo/5
    public async Task Put(Guid id, Todo todo)
    {
      await this.todoService.SetCompletedAsync(id);
    }

    public async Task Delete(Guid id)
    {
      await this.todoService.Delete(id);
    }
  }
}
