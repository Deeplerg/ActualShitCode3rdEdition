using DataStructExplorer.Core;

namespace DataStructExplorer.Console.Stack.Operations;

public class PopStackOperation : IStackOperation
{
    public string Name => "Pop";

    public string? Execute(LinkedStack<object> stack)
    {
        var element = stack.Pop();
        return element.ToString();
    }
}