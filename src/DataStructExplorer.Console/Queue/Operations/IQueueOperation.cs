using DataStructExplorer.Core;

namespace DataStructExplorer.Console.Queue.Operations;

public interface IQueueOperation
{
    string Name { get; }
    
    string? Execute(LinkedQueue<object> stack);
}