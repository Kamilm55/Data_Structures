
// FIFO (First in First out)
// The first item added -> (enqueued)
// The first one removed ->  (dequeued).
namespace Data_Structures;

public class MyQueue<T>
{
    private T[] _array;
    private int _head;       // Index of the first element
    private int _tail;       // Index to insert the next element
    private int _count;      // Current number of elements
    private int _capacity;   // Size of the array

    public MyQueue(int capacity = 4)
    {
        if (capacity < 1)
            throw new ArgumentException("Capacity must be at least 1."); // We must assign variables to 0th index 

        _capacity = capacity;
        _array = new T[_capacity];
        _head = 0;
        _tail = 0;
        _count = 0;
    }
    
    // Check if the queue is empty
    public bool IsEmpty() => _count == 0;
    
    // Number of items in the queue
    public int Count() => _count;
    
    // Add item to the queue
    public void Enqueue(T item)
    {
        
    }
    
    // Internal resize logic
    private void Resize()
    {
        
    }
}