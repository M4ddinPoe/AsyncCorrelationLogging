namespace AsyncCorrelationLogging.Domain
{
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Net.Http;
  using System.Net.Http.Formatting;
  using System.Threading.Tasks;
  using LogService;
  using Model;

  public class TodoRestRepository
  {
    private readonly Logger _logger;

    private readonly HttpClient _httpClient;

    public TodoRestRepository(Logger logger)
    {
      _logger = logger;
      _httpClient = new HttpClient();
    }

    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
      var uri = new Uri("http://localhost:49556/api/todo");
      var response = await this._httpClient.GetAsync(uri);

      if (response.StatusCode == HttpStatusCode.OK)
      {
        return await response.Content.ReadAsAsync<IEnumerable<Todo>>(new[] { new JsonMediaTypeFormatter() });
      }

      return null;
    }

    public async Task<Todo> GetByIdAsync(Guid id)
    {
      var uri = new Uri($"http://localhost:49556/api/todo/{id}");
      var response = await this._httpClient.GetAsync(uri);

      if (response.StatusCode == HttpStatusCode.OK)
      {
        return await response.Content.ReadAsAsync<Todo>(new[] { new JsonMediaTypeFormatter() });
      }

      return null;
    }

    public async Task AddAsync(Todo todo)
    {
      var uri = new Uri("http://localhost:49556/api/todo");
      var response = await this._httpClient.PostAsync(uri, todo, new JsonMediaTypeFormatter());

      if (response.StatusCode != HttpStatusCode.NoContent)
      {
        throw new Exception("Could not add todo.");
      }
    }

    public async Task UpdateAsync(Todo updatedTodo)
    {
      var uri = new Uri($"http://localhost:49556/api/todo/{updatedTodo.Id}");
      var response = await this._httpClient.PutAsync(uri, updatedTodo, new JsonMediaTypeFormatter());

      if (response.StatusCode != HttpStatusCode.NoContent)
      {
        throw new Exception("Could not update todo.");
      }
    }

    public async Task DeleteAsync(Todo todo)
    {
      var uri = new Uri($"http://localhost:49556/api/todo/{todo.Id}");
      var response = await this._httpClient.DeleteAsync(uri);

      if (response.StatusCode != HttpStatusCode.NoContent)
      {
        throw new Exception("Could not update todo.");
      }
    }
  }
}
