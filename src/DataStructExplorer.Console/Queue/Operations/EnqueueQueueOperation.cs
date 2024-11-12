using DataStructExplorer.Core;

namespace DataStructExplorer.Console.Queue.Operations;

public record class EnqueueQueueOperation(object Element) : IQueueOperation
{
    public string Name => "Вставка";

    public string? Execute(LinkedQueue<object> queue)
    {
        queue.Enqueue(Element);
        return Element.ToString();
    }
}