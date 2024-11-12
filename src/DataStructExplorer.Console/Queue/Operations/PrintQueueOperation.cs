using System.Text;
using DataStructExplorer.Core;

namespace DataStructExplorer.Console.Queue.Operations;

public class PrintQueueOperation : IQueueOperation
{
    public string Name => "Печать";

    public string? Execute(LinkedQueue<object> queue)
    {
        var builder = new StringBuilder();

        foreach (var element in queue)
        {
            builder.Append(element);
            builder.Append(' ');
        }

        return builder.ToString();
    }
}