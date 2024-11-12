using DataStructExplorer.Core;

namespace DataStructExplorer.Console.Stack.Operations;

public interface IStackOperation
{
    string Name { get; }
    
    string? Execute(LinkedStack<object> stack);
}