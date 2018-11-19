namespace AsyncCorrelationLogging.Domain
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using LogService;
  using Model;

  public class TodoService
  {
    private readonly Logger _logger;
    private readonly TodoRestRepository _todoRepository;

    public TodoService(Logger logger, TodoRestRepository todoRepository)
    {
      _logger = logger;
      _todoRepository = todoRepository;
    }

    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
      return await _todoRepository.GetAllAsync();
    }

    public async Task<Todo> GetByIdAsync(Guid id)
    {
      return await _todoRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Todo todo)
    {
      await _todoRepository.AddAsync(todo);
    }

    public async Task SetCompletedAsync(Todo todo)
    {
      _logger.Log($"Set todo ({todo.Id}) '{todo.Text}' to completed.");
      await _todoRepository.DeleteAsync(todo);
    }
  }
}
