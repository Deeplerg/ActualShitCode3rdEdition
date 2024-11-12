using DataStructExplorer.Console.Queue.Operations;
using DataStructExplorer.Core;
using Spectre.Console;

namespace DataStructExplorer.Console.Queue;

public class QueuePrograms
{
    private readonly string _defaultFileName;

    public QueuePrograms(string defaultFileName)
    {
        _defaultFileName = defaultFileName;
    }
    
    public void HandleQueueFile()
    {
        string input = Prompter.PromptFileNameAndReadWithoutNewLine(_defaultFileName);

        var operations = ParseOperations(input);

        var stack = new LinkedQueue<object>();
        foreach (var operation in operations)
        {
            string? operationResult = operation.Execute(stack);
            string operationName = operation.Name;

            if (operationResult is null)
            {
                AnsiConsole.WriteLine(operationName);
            }
            else
            {
                AnsiConsole.WriteLine($"{operationName}: {operationResult}");
            }
        }
    }
    
    private IEnumerable<IQueueOperation> ParseOperations(string input)
    {
        var split = input.Split(' ');

        foreach (string encodedOperation in split)
        {
            if (encodedOperation.StartsWith("1,"))
            {
                string element = encodedOperation.Substring(2);
                yield return new EnqueueQueueOperation(element);
                continue;
            }

            yield return encodedOperation switch
            {
                "2" => new DequeueQueueOperation(),
                "3" => new PeekQueueOperation(),
                "4" => new IsEmptyQueueOperation(),
                "5" => new PrintQueueOperation(),
                _ => throw new ArgumentException($"Неизвестная операция {encodedOperation}!")
            };
        }
    }
}