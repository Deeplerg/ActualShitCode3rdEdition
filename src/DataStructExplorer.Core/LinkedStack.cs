using System.Collections;

namespace DataStructExplorer.Core;

// LIFO
public class LinkedStack<T> : IEnumerable<T>
{
    private StackNode<T>? _top;

    public void Push(T value)
    {
        if (_top is null)
        {
            _top = new StackNode<T>(value);
            return;
        }

        var newTop = new StackNode<T>(value);
        newTop.Next = _top;
        _top = newTop;
    }

    public T Pop()
    {
        ThrowIfEmpty();
        
        var previousTop = _top;
        _top = previousTop!.Next;
        return previousTop.Value;
    }

    /// <summary>
    /// Returns the element at the top of the stack without removing it. 
    /// </summary>
    /// <returns>The element at the top of the stack.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the stack is empty.</exception>
    public T Top()
    {
        ThrowIfEmpty();
        
        return _top!.Value;
    }

    public bool IsEmpty()
    {
        return _top is null;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new LinkedStackEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    private void ThrowIfEmpty()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Stack is empty.");
    }
    
    // this class needs to have access to its parent class' private fields which is why it's here
    private class LinkedStackEnumerator : IEnumerator<T>
    {
        private readonly LinkedStack<T> _stack;
        private StackNode<T>? _currentNode;

        public LinkedStackEnumerator(LinkedStack<T> stack)
        {
            _stack = stack;
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
                _currentNode = _stack._top;
                return _currentNode is not null; // true if it's not null now
            }

            if (_currentNode.Next is null)
                return false;

            _currentNode = _currentNode.Next;
            return true;
        }

        public void Reset()
        {
            _currentNode = _stack._top;
        }

        public void Dispose()
        {
        }
    }
}