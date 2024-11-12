using DataStructExplorer.Core;

namespace DataStructExplorer.Console.Stack.Operations;

public class TopStackOperation : IStackOperation
{
    public string Name => "Top";

    public string? Execute(LinkedStack<object> stack)
    {
        try
        {
            var element = stack.Top();
            return element.ToString();
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
}