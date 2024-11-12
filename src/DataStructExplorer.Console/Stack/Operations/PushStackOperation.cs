using DataStructExplorer.Core;

namespace DataStructExplorer.Console.Stack.Operations;

public record class PushStackOperation(object Element) : IStackOperation
{
    public string Name => "Push";

    public string? Execute(LinkedStack<object> stack)
    {
        stack.Push(Element);
        return Element.ToString();
    }
}