using System.Text;
using DataStructExplorer.Core;

namespace DataStructExplorer.Console.Stack.Operations;

public class PrintStackOperation : IStackOperation
{
    public string Name => "Print";

    public string? Execute(LinkedStack<object> stack)
    {
        var builder = new StringBuilder();

        foreach (var element in stack)
        {
            builder.Append(element);
            builder.Append(' ');
        }

        return builder.ToString();
    }
}