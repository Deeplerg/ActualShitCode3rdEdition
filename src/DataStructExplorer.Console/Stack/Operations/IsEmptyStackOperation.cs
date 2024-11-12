using DataStructExplorer.Core;

namespace DataStructExplorer.Console.Stack.Operations;

public class IsEmptyStackOperation : IStackOperation
{
    public string Name => "IsEmpty";

    public string? Execute(LinkedStack<object> stack)
    {
        bool isEmpty = stack.IsEmpty();
        return isEmpty.ToString();
    }
}