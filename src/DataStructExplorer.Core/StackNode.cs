namespace DataStructExplorer.Core;

internal class StackNode<T>
{
    public T Value { get; private set; }
    public StackNode<T>? Next { get; set; }

    public StackNode(T value)
    {
        Value = value;
    }
}