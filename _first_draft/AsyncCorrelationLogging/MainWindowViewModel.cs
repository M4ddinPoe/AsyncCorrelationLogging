namespace AsyncCorrelationLogging
{
  using System;
  using System.Collections.ObjectModel;
  using System.Threading.Tasks;
  using System.Windows;
  using System.Windows.Input;
  using Domain;
  using LogService;
  using Model;
  using Prism.Commands;
  using Prism.Mvvm;

  public class MainWindowViewModel : BindableBase 
  {
    private readonly Logger _logger;
    private readonly TodoService _todoService;
    private string _newTaskText;

    public MainWindowViewModel(Logger logger, TodoService todoService)
    {
      _logger = logger;
      _todoService = todoService;

      NewTaskText = string.Empty;
      Todos = new ObservableCollection<Todo>();

      LoadedCommand = new DelegateCommand(ExecuteLoadedAsync);
      AddCommand = new DelegateCommand(ExecuteAddTodo);
      TaskCompletedCommand = new DelegateCommand<Todo>(ExecuteTodoCompletedAsync);
    }

    public ICommand LoadedCommand { get; }

    public ICommand AddCommand { get; }

    public ICommand TaskCompletedCommand { get; }

    public string NewTaskText
    {
      get
      {
        return _newTaskText;
      }

      set
      {
        SetProperty(ref _newTaskText, value);
      }
    }

    public ObservableCollection<Todo> Todos { get; }

    private async void ExecuteLoadedAsync()
    {
      await LoadTodosAsync();
    }

    private async void ExecuteTodoCompletedAsync(Todo todo)
    {
      _logger.Log($"Set todo completed: {todo.Text}");

      await _todoService.SetCompletedAsync(todo);
      await LoadTodosAsync();
    }

    private async void ExecuteAddTodo()
    {
      try
      {
        _logger.Log($"Add new todo: {NewTaskText}");

        await _todoService.AddAsync(new Todo(Guid.NewGuid(), NewTaskText));
        await LoadTodosAsync();

        NewTaskText = string.Empty;
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private async Task LoadTodosAsync()
    {
      Todos.Clear();

      var todos = await _todoService.GetAllAsync();
      Todos.AddRange(todos);
    }
  }
}
