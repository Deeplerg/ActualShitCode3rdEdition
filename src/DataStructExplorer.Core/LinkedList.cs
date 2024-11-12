namespace DataStructExplorer.Core;

public class LinkedList
{
    public LinkedListNode? Head { get; set; }

    public void AddLast(int data)
    {
        var newLinkedListNode = new LinkedListNode(data);
        if (Head is null)
        {
            Head = newLinkedListNode;
            return;
        }
        
        LinkedListNode current = Head;
        while (current.Next is not null)
        {
            current = current.Next;
        }
        current.Next = newLinkedListNode;
    }
}