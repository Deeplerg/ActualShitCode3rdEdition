using System.Collections;

namespace DataStructExplorer.Core;

// FIFO
public class LinkedQueue<T> : IEnumerable<T>
{
    private QueueNode<T>? _head;
    private QueueNode<T>? _tail;
    
    public void Enqueue(T value)
    {
        if (_head is null)
        {
            _head = new QueueNode<T>(value);
            _tail ??= _head;
            return;
        }

        var newTail = new QueueNode<T>(value);
        // tail should never be null unless head is also null
        _tail!.Next = newTail;
        _tail = newTail;
    }

    public T Dequeue()
    {
        ThrowIfEmpty();

        if (_head == _tail)
        {
            var value = _head!.Value;
            _head = null;
            _tail = null;
            return value;
        }

        var lastHead = _head!;
        _head = _head!.Next;
        return lastHead.Value;
    }

    public bool IsEmpty()
    {
        return _head is null;
    }

    /// <summary>
    /// Returns the element at the beginning of the queue without removing it. 
    /// </summary>
    /// <returns>The element at the beginning of the queue.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public T Peek()
    {
        ThrowIfEmpty();

        return _head!.Value;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new QueueEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private void ThrowIfEmpty()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Queue is empty.");
    }

    // this class needs to have access to its parent class' private fields which is why it's here
    private class QueueEnumerator : IEnumerator<T>
    {
        private readonly LinkedQueue<T> _queue;
        private QueueNode<T>? _currentNode;

        public QueueEnumerator(LinkedQueue<T> queue)
        {
            _queue = queue;
        }

        public T Current
        {
            get
            {
                if (_currentNode is null)
                    throw new InvalidOperationException("Enumeration hasn't started.");

                return _currentNode.Value;
            }
        }

        object? IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_currentNode is null)
            {
                _currentNode = _queue._head;
                return _currentNode is not null; // true if it's not null now
            }

            if (_currentNode.Next is null)
                return false;

            _currentNode = _currentNode.Next;
            return true;
        }

        public void Reset()
        {
            _currentNode = _queue._head;
        }

        public void Dispose()
        {
        }
    }
}