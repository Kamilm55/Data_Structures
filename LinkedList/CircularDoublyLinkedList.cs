using System.Collections;
using System.Diagnostics;

namespace LinkedList;

public class CircularDoublyLinkedList<T> : IEnumerable<CircularDoublyLinkedList<T>.Node<T>?>
{
    public Node<T>? Head { get; private set; } // First Node
    private int _size;

    public CircularDoublyLinkedList()
    {
        _size = 0;
    }
    
    public int Length => _size;
    public Node<T>? First => Head;
    public Node<T>? Last
    {
        get
        {
            if (Head != null) return Head.Prev;
            return null;
        }
    }


    public void  Add(T value)
    {
        AddLast(value);
    }
    
    public void AddFirst(T value)
    {
        Node<T> newNode = new Node<T>(this,value);

        InternalAddFirst(newNode);
    }
    
    public void AddFirst(Node<T> newNode)
    {
        ValidateNewNode(newNode);
        InternalAddFirst(newNode);

        newNode.List = this;
    }
    private void InternalAddFirst(Node<T> newNode)
    {
        if (Head == null)
        {
            InsertFirstNode(newNode);
        }
        else
        {
            InsertNodeExceptFirst(newNode);

            Head = newNode;// Change Head
        }
    }
    
    public void AddLast(T value)
    {
        Node<T> newNode = new Node<T>(this,value);

        InternalAddLast(newNode);
    }
    public void AddLast(Node<T> newNode)
    {
        ValidateNewNode(newNode);
        InternalAddLast(newNode);

        newNode.List = this;
    }

    private void InternalAddLast(Node<T> newNode)
    {
        if (Head == null) 
            InsertFirstNode(newNode);
        else
            InsertNodeExceptFirst(newNode);
    }

    private void InsertFirstNode(Node<T> newNode)
    {
        // First node in the list
        Head = newNode;
        Head.Next = Head;
        Head.Prev = Head;
        
        _size++;
    }

    private void InsertNodeExceptFirst(Node<T> newNode)
    {
        ValidateNode(newNode);
        
        Debug.Assert(Head != null, "This cannot be work whenever we insert first node -> Head == null we must call InsertFirstNode()"); // “Make sure that Head is not null at this point. If it is null, something is wrong — we should have called InsertFirstNode() instead.” 
        
        Node<T> tail = Head!.Prev!;

        Head.Prev = newNode;
        tail.Next = newNode;
            
        newNode.Prev = tail;
        newNode.Next = Head;
        
        _size++;
    }

    public void Clear()
    {
        if (Head == null) return; // No need to clear -> empty list
        
        // Head-den basqa hamisini invalidate etsek
        // Loop-dan sonra da yalniz head qalacaq, Head-i de invalidate edib, reference-i null etmek lazimdir
        
        var copyHead = Head;
        
        do
        {
            var temp = copyHead;
            copyHead = copyHead.Next!;
            
            if(temp != Head)
                InvalidateNode(temp);
           
        } while (copyHead != Head);
        
        InvalidateNode(Head);
        Head = null;
    }

    public Node<T>? Find(T value)
    {
        var copyHead = Head;
        if (copyHead == null) return null;
        
        do
        {
            if (copyHead != null && copyHead.Value != null && copyHead.Value.Equals(value))
            {
                return copyHead;
            }
            copyHead = copyHead.Next!;
            
        } while (copyHead != Head); // loop ends when you circle back
        return null;
    }
    
    public void Display()
    {
        if (Head == null)
        {
            Console.WriteLine("[]");
            return;
        }

        var copyHead = Head;
        
        Console.WriteLine("Print with normal order");
        do
        {
           Console.Write(copyHead.Value + " -> ");
           copyHead = copyHead.Next!;
           
        } while (copyHead != Head); // loop ends when you circle back
        
        Console.WriteLine("END");
        
        Console.WriteLine("Print with reverse order");
        var tail = Head.Prev;
        do
        {
            Console.Write(tail!.Value + " -> ");
            tail = tail!.Prev!;
           
        } while (tail != Head.Prev);// loop ends when you circle back
        Console.WriteLine("START");
    }


    public Node<T> AddAfter(Node<T> node, T value)
    {
        ValidateNode(node);
        var newNode = new Node<T>(this, value);
        
        InternalInsertNodeAfter(node,newNode);
        
        return newNode;
    }

    public Node<T> AddAfter(Node<T> node,Node<T> newNode)
    {
        ValidateNode(node);
        ValidateNewNode(newNode);
        
        InternalInsertNodeAfter(node,newNode);
        
        return newNode;
    }

    public Node<T> AddBefore(Node<T> node, T value)
    {
        ValidateNode(node);
        var newNode = new Node<T>(this, value);
        
        InternalInsertNodeBefore(node,newNode);
        return newNode;
    }

    public Node<T> AddBefore(Node<T> node,Node<T> newNode)
    {
        ValidateNode(node);
        ValidateNewNode(newNode);
        
        InternalInsertNodeBefore(node,newNode);
        return newNode;
    }

    private void InternalInsertNodeBefore(Node<T> currentNode, Node<T> newNode)
    {
        Debug.Assert(currentNode != null, "currentNode != null -> must be true in this method, we check in validateNode" );

        newNode.Next = currentNode;
        newNode.Prev = currentNode.Prev;
        
        currentNode.Prev!.Next = newNode;
        currentNode.Prev = newNode;
        
        if (currentNode == Head) Head = newNode;
        _size++;
    }

    private void ValidateNewNode(Node<T> node)
    {
        ArgumentNullException.ThrowIfNull(node);

        if (node.List != null)
            throw new InvalidOperationException("New node is already belong to the specific list, you cannot add to this linked list");
    }

    private void InternalInsertNodeAfter(Node<T> currentNode, Node<T> newNode)
    {
        Debug.Assert(currentNode != null, "currentNode != null -> must be true in this method, we check in validateNode" );
        
        newNode.Next = currentNode.Next;
        newNode.Prev = currentNode;
        
        currentNode.Next!.Prev = newNode;
        currentNode.Next = newNode;

        _size++;
    }

    public void Insert(T newValue, int index)
    {
        if(index == 0){ AddFirst(newValue); return; }
        if(index == _size){ AddLast(newValue); return; }
        
        // Insert into middle
        var newNode = new Node<T>(this,newValue);
        
        if (Head == null)
        {
            InsertFirstNode(newNode);
            return;
        }
        
        var nodeAtIndex = FindByIndex(index);
        
        nodeAtIndex.Prev.Next = newNode;
        newNode.Prev = nodeAtIndex.Prev;
        
        newNode.Next = nodeAtIndex;
        nodeAtIndex.Prev = newNode;
        
        _size++;
    }

    public bool Contains(T value)
    {
        return Find(value) != null;
    }
    private Node<T>? FindByIndex(int index)
    {
        if (index < 0 || index > _size) throw new ArgumentOutOfRangeException(nameof(index));
        
        var copyHead = Head;

        for (int i = 0; i < index; i++)
        {
            copyHead = copyHead.Next!;
        }
        
        return copyHead;// If index = 0 -> loop does not work and return Head
    }
    
    public void RemoveFirst()
    {
        if (Head == null) { throw new InvalidOperationException("You cannot remove element from empty list"); }

        InternalRemoveNode(Head);
    }

    public void RemoveLast()
    {
        if (Head == null) { throw new InvalidOperationException("You cannot remove element from empty list"); }
        
        InternalRemoveNode(Head.Prev!);
    }

    public bool Remove(T value)
    {
        var nodeToRemove = Find(value);
        
        if (nodeToRemove == null) return false;
        
        InternalRemoveNode(nodeToRemove);
        return true;
    }
    
    public void Remove(Node<T> node)
    {
        ValidateNode(node);
        InternalRemoveNode(node);
    }

    private void ValidateNode(Node<T> node)
    {
        ArgumentNullException.ThrowIfNull(node);

        if (node.List != this)
        {
            throw new InvalidOperationException("This node is not belong to this linked list");
        }
    }

    private void InternalRemoveNode(Node<T> currentNode)
    {
        Debug.Assert(currentNode.List == this, "Deleting the node from another list!");
        Debug.Assert(Head != null, "This method shouldn't be called on empty list!");
        
        if (currentNode.Next == currentNode) // This means list contains only one element
        {
            Debug.Assert(_size == 1 && Head == currentNode, "this should only be true for a list with only one node");
            Head = null;
        }
        else
        {
            currentNode.Next!.Prev = currentNode.Prev;
            currentNode.Prev!.Next = currentNode.Next;

            if (currentNode == Head) // If deleted element is Head, otherwise not change Head
                Head = currentNode.Next;
        }
        
        
        InvalidateNode(currentNode);
        _size--;
    }

    private void InvalidateNode(Node<T>? node)
    {
        node!.Next = null;
        node.Prev = null;
        node.List = null;
        node.Value = default;
    }
    
    public IEnumerator<Node<T>> GetEnumerator()
    {
        if (Head == null)
            yield break; // stops the iterator without yielding anything

        var current = Head;

        do
        {
            yield return current;
            current = current.Next!;
        } while (current != Head);
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public class Node<T>
    {
        public CircularDoublyLinkedList<T>? List{ get; set; }
        public T? Value { get; set; }
        public Node<T>? Prev { get; set; }
        public Node<T>? Next { get; set; }


        public Node(CircularDoublyLinkedList<T> list, T? value)
        {
            List = list;
            Value = value;
        }
        public Node(T? value, Node<T>? prev, Node<T>? next)
        {
            Value = value;
            Prev = prev;
            Next = next;
        }

        public Node(T? value, Node<T>? next)
        {
            Value = value;
            Next = next;
        }

        public Node(T? value)
        {
            Value = value;
        }
    }
}