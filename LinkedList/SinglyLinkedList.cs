namespace LinkedList;

public class SinglyLinkedList<T>
{
    public Node<T>? Head {get; private set;} // First Node
    public Node<T>? Tail {get; private set;} // Last Node
    private int _size;

    public SinglyLinkedList()
    {
        _size = 0;
    }


    // Add node to beginning of the LinkedList -> O(1)
    public void AddFirst(T val)
    {
        Node<T> newNode = new Node<T>(val);
        newNode.Next = Head; 
        Head = newNode;
        
        if(Tail == null) // Tail is null only adding first node into linked list --> If we don't do this, whenever we want to insert end of the linked list we must use Tail.Next -> Tail cannot be null
            Tail = Head;
        
        _size++;
    }
    
    // Add node to end of the LinkedList -> O(1) due to the Tail
    public void AddLast(T val)
    {
        if (Tail == null) AddFirst(val);
        else
        {
            Node<T> newNode = new Node<T>(val);
            Tail.Next = newNode;
            Tail = newNode;
            
            _size++;
        }
    }
    
    // Insert into specific index -> O(N)
    public void Insert(T newValue, int index)
    {
        if (index == 0) { AddFirst(newValue); return; }
        if (index == _size) { AddLast(newValue); return; }
        if (index < 0 || index > _size) throw new ArgumentOutOfRangeException(nameof(index));

        Node<T> newNode = new Node<T>(newValue);
        var current = GetNodeBeforeIndex(index);

        newNode.Next = current.Next;
        current.Next = newNode;
        _size++;
    }

    private Node<T> GetNodeBeforeIndex(int index)
    {
        Node<T> current = Head; // Temp variable to indicate current copy of node
        
        // after the loop current is 1 element before the index
        for (int i = 0; i < index - 1; i++)
        {
            current = current.Next;
        }

        return current;
    }

    public bool Remove(T value)
    {
        var previousNodeToRemove = FindPreviousNodeByValue(value);

        if (previousNodeToRemove != null)
        {
            if(previousNodeToRemove.Next == null) { RemoveLast(); return true;} 
            if(Head == previousNodeToRemove) { RemoveFirst(); return true;}

            // For middle deletions
            RemoveByPreviousNode(previousNodeToRemove);

            return true;
        }
        return false;
    }

    private void RemoveByPreviousNode(Node<T> previousNodeToRemove)
    {
        // Copy removed element
        var nodeToRemove = previousNodeToRemove.Next;
            
        // Remove middle elements -> break the chain and relate with the next of removed
        previousNodeToRemove.Next = previousNodeToRemove.Next?.Next;
            
        // Clear removed nodes references to help gc
        nodeToRemove!.Next = null;
        nodeToRemove.Value = default;

        _size--;
    }

    public void RemoveLast()
    {
        var previousNodeToRemove = GetNodeBeforeIndex(_size - 1);
        RemoveByPreviousNode(previousNodeToRemove);
        Tail = previousNodeToRemove.Next;
    }
    public void RemoveFirst()
    {
        if (Head == null) throw new InvalidOperationException("You cannot remove from empty linked list");
        
        var copyHeadNext = Head.Next;
        Head.Next = null;
        Head.Value = default;
        
        Head = copyHeadNext;
        _size--;
    }

    private Node<T>? FindPreviousNodeByValue(T value)
    {
        Node<T> current = Head; // Temp variable to indicate current copy of node

        while (current != null)
        {
            if(current.Next != null && current.Next.Value != null && current.Next.Value.Equals(value))
                return current;
            
            current = current.Next;
        }

        return null;
    }


    // O(N)
    public void Display()
    {
        if (Head == null)
        {
            Console.WriteLine("[]");
            return;
        }

        var copyHead = Head;
        while (copyHead != null)
        {
            Console.Write(copyHead.Value + " ");
            copyHead = copyHead.Next;
            
        }     
    }

    public class Node<T>
    {
        public T? Value {get; set;}
        public Node<T>? Next {get;set;}

        public Node(T? value, Node<T>? next)
        {
            this.Value = value;
            Next = next;
        }

        public Node(T? value)
        {
            this.Value = value;
        }
    }

}