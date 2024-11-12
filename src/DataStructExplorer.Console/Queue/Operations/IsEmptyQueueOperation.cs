using DataStructExplorer.Core;

namespace DataStructExplorer.Console.Queue.Operations;

public class IsEmptyQueueOperation : IQueueOperation
{
    public string Name => "Проверка на пустоту";

    public string? Execute(LinkedQueue<object> stack)
    {
        bool isEmpty = stack.IsEmpty();
        return isEmpty ? "Да" : "Нет";
    }
}