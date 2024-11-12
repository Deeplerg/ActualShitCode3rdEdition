namespace DataStructExplorer.Console.LinkedList;

using Core;

public class LinkedListOperations
{
    // 1. Reverse the list
    public static void Reverse(LinkedList list)
    {
        LinkedListNode? prev = null, current = list.Head, next;
        while (current is not null)
        {
            next = current.Next;
            current.Next = prev;
            prev = current;
            current = next;
        }
        list.Head = prev;
    }

    // 2. Move last element to start (or first to end)
    public static void MoveLastToStart(LinkedList list)
    {
        if (list.Head is null || list.Head.Next is null) return;

        LinkedListNode current = list.Head;
        while (current.Next?.Next is not null)
        {
            current = current.Next;
        }
        
        LinkedListNode? lastLinkedListNode = current.Next;
        current.Next = null;
        if (lastLinkedListNode is not null)
        {
            lastLinkedListNode.Next = list.Head;
            list.Head = lastLinkedListNode;
        }
    }

    public static void MoveFirstToEnd(LinkedList list)
    {
        if (list.Head is null || list.Head.Next is null) return;

        var firstLinkedListNode = list.Head;
        list.Head = firstLinkedListNode.Next;
        firstLinkedListNode.Next = null;

        var current = list.Head;
        while (current.Next is not null)
        {
            current = current.Next;
        }
        current.Next = firstLinkedListNode;
    }

    // 3. Count unique elements
    public static int CountUniqueElements(LinkedList list)
    {
        var seen = new HashSet<int>();
        var current = list.Head;
        while (current is not null)
        {
            seen.Add(current.Data);
            current = current.Next;
        }
        return seen.Count;
    }

    // 4. Remove non-unique elements
    public static void RemoveNonUniqueElements(LinkedList list)
    {
        var elementCount = new Dictionary<int, int>();
        var current = list.Head;

        while (current is not null)
        {
            if (elementCount.ContainsKey(current.Data))
                elementCount[current.Data]++;
            else
                elementCount[current.Data] = 1;

            current = current.Next;
        }

        current = list.Head;
        LinkedListNode? prev = null;

        while (current is not null)
        {
            if (elementCount[current.Data] > 1)
            {
                if (prev is null)
                    list.Head = current.Next;
                else
                    prev.Next = current.Next;
            }
            else
                prev = current;

            current = current.Next;
        }
    }

    // 5. Insert list L after first occurrence of x
    public static void InsertListAfterFirstX(LinkedList list, LinkedList toInsert, int x)
    {
        var current = list.Head;
        while (current is not null && current.Data != x)
        {
            current = current.Next;
        }

        if (current is null) return;

        var temp = current.Next;
        current.Next = toInsert.Head;

        var insertEnd = toInsert.Head;
        while (insertEnd?.Next is not null)
        {
            insertEnd = insertEnd.Next;
        }

        if (insertEnd is not null) 
            insertEnd.Next = temp;
    }

    // 6. Insert element E in sorted order
    public static void InsertInSortedOrder(LinkedList list, int e)
    {
        LinkedListNode newLinkedListNode = new LinkedListNode(e);

        if (list.Head is null || list.Head.Data >= e)
        {
            newLinkedListNode.Next = list.Head;
            list.Head = newLinkedListNode;
            return;
        }

        LinkedListNode current = list.Head;
        while (current.Next is not null && current.Next.Data < e)
        {
            current = current.Next;
        }

        newLinkedListNode.Next = current.Next;
        current.Next = newLinkedListNode;
    }

    // 7. Remove all elements with value E
    public static void RemoveAllElements(LinkedList list, int e)
    {
        while (list.Head is not null && list.Head.Data == e)
        {
            list.Head = list.Head.Next;
        }

        var current = list.Head;
        while (current?.Next is not null)
        {
            if (current.Next.Data == e)
                current.Next = current.Next.Next;
            else
                current = current.Next;
        }
    }

    // 8. Insert F before the first occurrence of E
    public static void InsertBeforeFirstE(LinkedList list, int e, int f)
    {
        if (list.Head is null) return;

        if (list.Head.Data == e)
        {
            LinkedListNode newLinkedListNode = new LinkedListNode(f);
            newLinkedListNode.Next = list.Head;
            list.Head = newLinkedListNode;
            return;
        }

        LinkedListNode current = list.Head;
        while (current.Next is not null && current.Next.Data != e)
        {
            current = current.Next;
        }

        if (current.Next is not null)
        {
            LinkedListNode newLinkedListNode = new LinkedListNode(f);
            newLinkedListNode.Next = current.Next;
            current.Next = newLinkedListNode;
        }
    }

    // 9. Append list E to list L
    public static void AppendList(LinkedList list, LinkedList toAppend)
    {
        if (list.Head is null)
        {
            list.Head = toAppend.Head;
            return;
        }

        LinkedListNode current = list.Head;
        while (current.Next is not null)
        {
            current = current.Next;
        }
        current.Next = toAppend.Head;
    }

    // 10. Split list by first occurrence of x
    public static (LinkedList, LinkedList) SplitByFirstX(LinkedList list, int x)
    {
        var beforeX = new LinkedList();
        var afterX = new LinkedList();

        LinkedListNode? current = list.Head;
        LinkedListNode? beforeCurrent = null;

        while (current is not null)
        {
            if (current.Data == x)
            {
                afterX.Head = current;
                if (beforeCurrent is not null)
                {
                    beforeCurrent.Next = null;
                }
                else
                {
                    beforeX.Head = null;
                }
                break;
            }

            if (beforeX.Head is null)
                beforeX.Head = current;
            beforeCurrent = current;
            current = current.Next;
        }

        if (current is null)
        {
            beforeX.Head = list.Head;
        }

        return (beforeX, afterX);
    }

    // 11. Double the list
    public static void DoubleList(LinkedList list)
    {
        if (list.Head is null) return;

        LinkedListNode current = list.Head;
        while (current.Next is not null)
        {
            current = current.Next;
        }

        LinkedListNode newHead = list.Head;
        current.Next = CloneList(newHead);
    }

    private static LinkedListNode? CloneList(LinkedListNode? head)
    {
        if (head is null) return null;
        LinkedListNode newHead = new LinkedListNode(head.Data);
        newHead.Next = CloneList(head.Next);
        return newHead;
    }

    // 12. Swap two elements by value
    public static void SwapElements(LinkedList list, int value1, int value2)
    {
        if (list.Head is null || value1 == value2) return;

        LinkedListNode? node1Prev = null, node2Prev = null;
        LinkedListNode? node1 = list.Head, node2 = list.Head;

        while (node1 is not null && node1.Data != value1)
        {
            node1Prev = node1;
            node1 = node1.Next;
        }

        while (node2 is not null && node2.Data != value2)
        {
            node2Prev = node2;
            node2 = node2.Next;
        }

        if (node1 is null || node2 is null) return;

        if (node1Prev is not null)
            node1Prev.Next = node2;
        else
            list.Head = node2;

        if (node2Prev is not null)
            node2Prev.Next = node1;
        else
            list.Head = node1;

        LinkedListNode? temp = node1.Next;
        node1.Next = node2.Next;
        node2.Next = temp;
    }
}
