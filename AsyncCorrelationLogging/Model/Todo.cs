namespace AsyncCorrelationLogging.Model
{
  using System;

  public sealed class Todo
  {
    public Todo()
    {
      Id = Guid.Empty;
      Text = string.Empty;
    }

    public Todo(Guid id, string text)
    {
      Id = id;
      Text = text;
    }

    public Guid Id { get; set; }

    public string Text { get; set; }
  }
}
