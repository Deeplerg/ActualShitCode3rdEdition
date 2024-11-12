using DataStructExplorer.Core;

namespace DataStructExplorer.Console.Queue.Operations;

public class DequeueQueueOperation : IQueueOperation
{
    public string Name => "Удаление";

    public string? Execute(LinkedQueue<object> queue)
    {
        var element = queue.Dequeue();
        return element.ToString();
    }
}