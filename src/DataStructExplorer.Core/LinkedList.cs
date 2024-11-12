namespace DataStructExplorer.Core;

public class LinkedList
{
    public LinkedListNode? Head { get; set; }

    public void AddLast(int data)
    {
        LinkedListNode newLinkedListNode = new LinkedListNode(data);
        if (Head == null)
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

    public void PrintList()
    {
        var current = Head;
        while (current is not null)
        {
            Console.Write(current.Data + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }
}