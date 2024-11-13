namespace DataStructExplorer.Console.LinkedList;

public class TodoItem
{
    public TimeSpan Time;
    public string Task;

    public TodoItem(TimeSpan time, string task)
    {
        Time = time;
        Task = task;
    }

    public override string ToString()
    {
        return $"[{Time}] {Task}";
    }
}