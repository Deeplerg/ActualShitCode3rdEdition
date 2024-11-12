namespace DataStructExplorer.Core;

internal class QueueNode<T>
{
    public T Value { get; private set; }
    public QueueNode<T>? Next { get; set; }

    public QueueNode(T value)
    {
        Value = value;
    }
}