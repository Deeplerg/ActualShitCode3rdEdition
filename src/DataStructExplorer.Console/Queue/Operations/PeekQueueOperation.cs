using DataStructExplorer.Core;

namespace DataStructExplorer.Console.Queue.Operations;

public class PeekQueueOperation : IQueueOperation
{
    public string Name => "Вывод первого элемента";

    public string? Execute(LinkedQueue<object> queue)
    {
        try
        {
            var element = queue.Peek();
            return element.ToString();
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
}