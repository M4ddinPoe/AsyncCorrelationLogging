namespace AsyncCorrelationLogging.RestApi.Domain
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using LogService;
  using Models;

  public class TodoService
  {
    private readonly Logger _logger;
    private readonly TodoJsonRepository _todoRepository;

    public TodoService(Logger logger, TodoJsonRepository todoRepository)
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

    public async Task UpdateAsync(Todo todo)
    {
      await this._todoRepository.UpdateAsync(todo);
    }

    public async Task SetCompletedAsync(Guid id)
    {
      Todo todo = await _todoRepository.GetByIdAsync(id);

      _logger.Log($"Set todo ({id}) '{todo.Text}' to completed.");

      await _todoRepository.DeleteAsync(todo);
    }

    public async Task Delete(Guid id)
    {
      var todo = await GetByIdAsync(id);
      await this._todoRepository.DeleteAsync(todo);
    }
  }
}
