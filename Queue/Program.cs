
// FIFO (First in First out)
// The first item added -> (enqueued)
// The first one removed ->  (dequeued).

// tail -> ... -> ... -> ... -> head

using System.Collections;

namespace Data_Structures;

public class Startup
{
    static void Main()
    {
        Console.WriteLine("------------ System Queue -------------");
        Queue<int> sysQueue = new Queue<int>();
        sysQueue.Enqueue(1);
        sysQueue.Enqueue(2);
        sysQueue.Enqueue(3);
        sysQueue.Enqueue(4);
        sysQueue.Enqueue(45);// Resize applied

        foreach (var s in sysQueue.ToArray())
        {
            Console.WriteLine(s);
        }
        
        Console.WriteLine("------------ System Queue CopyTo method -------------");
        int[] arr = new int[7];
        sysQueue.CopyTo(arr, 1);
        foreach (var s in arr.ToArray())
        {
            Console.WriteLine(s);
        }
        
        Console.WriteLine("------------  -------------");
        
        Console.WriteLine("Peek: " + sysQueue.Peek());
        
        Console.WriteLine(sysQueue.Contains(45));
        Console.WriteLine(sysQueue.Contains(452));
        
        Console.WriteLine(sysQueue.Dequeue());
        Console.WriteLine(sysQueue.Dequeue());
        Console.WriteLine(sysQueue.Dequeue());
        Console.WriteLine(sysQueue.Dequeue());
        Console.WriteLine(sysQueue.Dequeue());
        
        //sysQueue.Dequeue();
        
        Console.WriteLine("------------ Custom Queue -------------");
        MyQueue<int> myQueue = new MyQueue<int>();
        myQueue.Enqueue(1);
        myQueue.Enqueue(2);
        myQueue.Enqueue(3);
        myQueue.Enqueue(4);
        myQueue.Enqueue(45);// Resize applied

        foreach (var s in myQueue.ToArray())
        {
            Console.WriteLine(s);
        }
        
        Console.WriteLine("------------ Custom Queue CopyTo method -------------");
        int[] arr2 = new int[7];
        myQueue.CopyTo(arr2, 1);
        foreach (var s in arr2.ToArray())
        {
            Console.WriteLine(s);
        }
        
        Console.WriteLine("------------  -------------");
        
        Console.WriteLine("Peek: " + myQueue.Peek());
        
        Console.WriteLine(myQueue.Contains(45));
        Console.WriteLine(myQueue.Contains(452));
        
        Console.WriteLine(myQueue.Dequeue());
        Console.WriteLine(myQueue.Dequeue());
        Console.WriteLine(myQueue.Dequeue());
        Console.WriteLine(myQueue.Dequeue());
        Console.WriteLine(myQueue.Dequeue());
        // myQueue.Clear();
       // myQueue.Dequeue();
       
    }
}
public class MyQueue<T> : IEnumerable<T>
{
    private T[] _array;
    private int _head;       // Next item to dequeue.
    private int _tail;       // Index to insert the next element (next empty slot for enqueueing)
    private int _size;      // Current number of elements, actual size without default
    private int _capacity;   // Capacity of the array

    public MyQueue(int capacity = 4)
    {
        if (capacity < 1)
            throw new ArgumentException("Capacity must be at least 1."); // We must assign variables to 0th index 

        _capacity = capacity;
        _array = new T[_capacity];
        _head = 0;
        _tail = 0;
        _size = 0;
    }
    
    
    // Add item to the queue -> O(1)
    public void Enqueue(T item)
    {
        if (IsFull())
        {
            Resize(ref _array, _array.Length * 2);
        }

        int indexToEnqueue = _tail % _array.Length;// if head is less than length nothing changes, otherwise add start of the arr
        _array[indexToEnqueue] = item;

        _tail++;
        _size++;
    }
    // Removes the object at the head of the queue and returns it. If the queue is empty, this method throws an InvalidOperationException.
    // O(1) due to the circular buffer
    public T Dequeue()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Queue is empty.");
        
        int indexToDequeue = _head % _array.Length;
        
        T removed = _array[indexToDequeue];
        _array[indexToDequeue] = default!;

        _head++;
        _size--;

        return removed;
    }

    private bool IsFull() => _array.Length == _size;
    
    // Check if the queue is empty
    public bool IsEmpty() => _size == 0;
    
    // Number of items in the queue
    public int Count() => _size;

    // Internal resize logic
    private void Resize(ref T[] srcArray, int newCapacity)
    {
      T[] newArr = new T[newCapacity];
      int i = 0;
      
      do
      {
          int headWithModulus = _head % _array.Length;
          newArr[i] = srcArray[headWithModulus];
          _head++;
          i++;

      } while (i != _tail);

      // Set default values for new created arr
      _head = 0;
      _tail = _size;
      
      srcArray = newArr;
    }
    
    // Removes all Objects from the queue.
    public void Clear()
    {
        for (int i = 0; i < _size; i++)
        {
            _array[i] = default!;
        }
        
        _size = 0;
        _head = 0;
        _tail = 0;
    }

    // Returns the object at the head of the queue. The object remains in the queue.
    // If the queue is empty, this method throws an InvalidOperationException.
    public T Peek()
    {
        if(IsEmpty()) 
           throw new InvalidOperationException("Queue is empty.");
        
        return _array[_head];
    }

    public bool Contains(T item)
    {
        bool contains = false;
        int i = 0;
        int copyHead = _head;
      
        do
        {
            int headWithModulus = copyHead % _array.Length;
            if (_array[headWithModulus]!.Equals(item)) return true;
            copyHead++;
            i++;
        } while (i != _tail);
        
       
        return contains;
    }

    public T[] ToArray()
    {
        T[] newArr = new T[_size];
        int i = 0;
        int copyHead =  _head;
        
        do
        {
            int headWithModulus = copyHead % _array.Length;
            newArr[i] = _array[headWithModulus];
            copyHead++;
            i++;

        } while (i != _tail);

        return newArr;
    }
    
    // CopyTo copies a collection into an Array, starting at a particular index into the array.
    public void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);
        
        if (arrayIndex < 0 || arrayIndex > array.Length)
        {
            throw new InvalidOperationException();
        }

        if (array.Length - arrayIndex < _size)
        {
            throw new InvalidOperationException("Queue size is greater than array length from offset(arrayIndex).");
        }
        
        int i = 0;
        int copyHead =  _head;
        
        int j = arrayIndex;// Offset/start index for array which we try to copy into

        do
        {
            int headWithModulus = copyHead % _array.Length;
            array[j] = _array[headWithModulus];
            
            copyHead++;
            i++;
            j++;
        } while (i != _tail);
    }

    public void TrimExcess()
    {
        int threshold = (int)(_array.Length * 0.9);
        if (_size < threshold)
        {
            Resize(ref _array,_size);
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _size; i++)
        {
           yield return _array[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}