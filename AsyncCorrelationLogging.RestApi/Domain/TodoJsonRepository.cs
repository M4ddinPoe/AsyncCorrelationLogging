namespace AsyncCorrelationLogging.RestApi.Domain
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Web;
  using LogService;
  using Models;
  using Newtonsoft.Json;

  public class TodoJsonRepository
  {
    private readonly Logger _logger;
    string _dataPath;

    public TodoJsonRepository(Logger logger)
    {
      _logger = logger;
      _dataPath = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, Path.Combine("App_Data", "todos.json"));
    }

    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
      return await Task.Run(() => GetAll());
    }

    public IEnumerable<Todo> GetAll()
    {
      IEnumerable<Todo> todos = JsonConvert.DeserializeObject<IEnumerable<Todo>>(File.ReadAllText(_dataPath));

      _logger.Log($"Get all returns {todos.Count()} items.");

      return todos;
    }

    public async Task<Todo> GetByIdAsync(Guid id)
    {
      return await Task.Run(() => GetById(id));
    }

    public Todo GetById(Guid id)
    {
      IEnumerable<Todo> todos = JsonConvert.DeserializeObject<IEnumerable<Todo>>(File.ReadAllText(_dataPath));
      Todo todo = todos.FirstOrDefault(t => t.Id == id);

      _logger.Log($"Get by id ({id}) returns '{todo?.Text}'.");

      return todo;
    }

    public async Task AddAsync(Todo todo)
    {
      await Task.Run(() => Add(todo));
    }

    public void Add(Todo todo)
    {
      List<Todo> todos = JsonConvert.DeserializeObject<List<Todo>>(File.ReadAllText(_dataPath));
      todos.Add(todo);

      SaveTodos(todos);

      _logger.Log($"Added new item ({todo.Id}) returns '{todo.Text}'.");
    }

    public async Task UpdateAsync(Todo updatedTodo)
    {
      await Task.Run(() => Update(updatedTodo));
    }

    public void Update(Todo updatedTodo)
    {
      List<Todo> todos = JsonConvert.DeserializeObject<List<Todo>>(File.ReadAllText(_dataPath));
      Todo originalTodo = GetById(updatedTodo.Id);

      if (originalTodo == null)
      {
        throw new InvalidOperationException($"Todo ({updatedTodo.Id}) '{updatedTodo.Text}' not found");
      }

      todos.Remove(originalTodo);
      todos.Add(updatedTodo);

      _logger.Log($"Updated item ({updatedTodo.Id}) from '{originalTodo.Text}' to '{updatedTodo.Text}'.");
    }

    public async Task DeleteAsync(Todo todo)
    {
      await Task.Run(() => Delete(todo));
    }

    public void Delete(Todo todo)
    {
      List<Todo> todos = JsonConvert.DeserializeObject<List<Todo>>(File.ReadAllText(_dataPath));
      Todo originalTodo = todos.FirstOrDefault(t => t.Id == todo.Id);

      todos.Remove(originalTodo);
      SaveTodos(todos);

      _logger.Log($"Removed item ({originalTodo?.Id}) from '{originalTodo?.Text}'.");
    }

    private void SaveTodos(List<Todo> todos)
    {
      string json = JsonConvert.SerializeObject(todos);
      File.WriteAllText(_dataPath, json);
    }
  }
}
