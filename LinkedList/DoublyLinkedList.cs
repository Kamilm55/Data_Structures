namespace LinkedList;

public class DoublyLinkedList<T> // Not circular
{
    public Node<T>? Head { get; private set; } // First Node
    private int _size;

    public DoublyLinkedList()
    {
        _size = 0;
    }


    public void AddFirst(T value)
    {
        Node<T> newNode = new Node<T>(value);

        newNode.Next = Head;
        newNode.Prev = null;
        if (Head != null)
            Head.Prev = newNode;
        
        Head = newNode;
        _size++;
    }

    public void AddLast(T value)
    {
        Node<T> newNode = new Node<T>(value);
        
        newNode.Next = null;
        if (Head == null) // First element to add
        {
            newNode.Prev = null;
            Head = newNode;
            _size++;
            return;
        }
        
        Node<T> lastNode = Head; 
        while (lastNode.Next != null) // Break when next element is null, one element before null
        {
            lastNode = lastNode.Next;
        }   
        
        lastNode.Next = newNode;
        newNode.Prev = lastNode;
        _size++;
    }

    public Node<T>? Find(int value)
    {
        var copyHead = Head;
        while (copyHead != null)
        {
            if (copyHead.Value != null && copyHead.Value.Equals(value))
                return copyHead;
            
            copyHead = copyHead.Next;
        }
        return null;
    }

    public void Insert(T newValue, int index)
    {
        if(index == 0){ AddFirst(newValue); return; }
        if(index == _size){ AddLast(newValue); return; }
        
        // Insert into middle
        var newNode = new Node<T>(newValue);
        
        var nodeAtIndex = FindByIndex(index);

        nodeAtIndex.Prev.Next = newNode;
        newNode.Prev = nodeAtIndex.Prev;
        
        newNode.Next = nodeAtIndex;
        nodeAtIndex.Prev = newNode;
        
        _size++;
    }

    private Node<T>? FindByIndex(int index)
    {
        if (index < 0 || index > _size) throw new ArgumentOutOfRangeException(nameof(index));
        
        int i = 0;
        var copyHead = Head;
        while (i < index && copyHead != null)
        {
            copyHead = copyHead.Next;
            i++;
        }
        
        return copyHead;
    }


    public void Display()
    {
        if (Head == null)
        {
            Console.WriteLine("[]");
            return;
        }

        var copyHead = Head;
        Node<T>? last = null; 
        Console.WriteLine("Print with normal order");
        while (copyHead != null)
        {
            Console.Write(copyHead.Value + " -> ");
            last = copyHead; //  one element before null -> it should not be null
            copyHead = copyHead.Next;// // In the end of the iteration it gives null
        }     
        Console.WriteLine("END");
        
        Console.WriteLine("Print with reverse order");
        while (last != null)
        {
            Console.Write(last.Value + " -> ");
            last = last.Prev;
        }
        Console.WriteLine("START");
    }
    
    public class Node<T>
    {
        public T? Value {get; set;}
        public Node<T>? Prev {get;set;}
        public Node<T>? Next {get;set;}
        

        public Node(T? value, Node<T>? prev, Node<T>? next)
        {
            this.Value = value;
            Prev = prev;
            Next = next;
        }
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