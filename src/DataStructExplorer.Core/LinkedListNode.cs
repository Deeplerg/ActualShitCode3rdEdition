namespace DataStructExplorer.Core;

public class LinkedListNode
{
    public int Data { get; set; }
    public LinkedListNode? Next { get; set; }

    public LinkedListNode(int data)
    {
        Data = data;
        Next = null;
    }
}
