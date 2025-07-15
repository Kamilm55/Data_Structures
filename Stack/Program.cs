// LIFO -> Last in First out

using System.Collections;

public class Startup
{
   static void Main(string[] args)
{
    Console.WriteLine("======= SYSTEM STACK TESTS =======");
    Stack<int> sysStack = new Stack<int>();

    Console.WriteLine("[System] Initial Count: " + sysStack.Count);

    // Push values
    Console.WriteLine("\n[System] Pushing values: 15, 10, 20, 30, 32");
    sysStack.Push(15);
    sysStack.Push(10);
    sysStack.Push(20);
    sysStack.Push(30);
    sysStack.Push(32);

    Console.WriteLine("[System] Count after Push: " + sysStack.Count);
    Console.WriteLine("--------------------- [System] Copy to ---------------------");
    int[] arr = new int[7];
    sysStack.CopyTo(arr, 1);
    foreach (var s in arr.ToArray())
    {
        Console.WriteLine(s);
    }
    Console.WriteLine("--------------------- ---------------- ---------------------");

    
    Console.WriteLine("\n[System] ToArray (LIFO):");
    int[] sysArray = sysStack.ToArray();
    foreach (var i in sysArray)
        Console.WriteLine($"  [System] {i}");

    Console.WriteLine($"\n[System] Pop: {sysStack.Pop()}");    // Should remove 32
    Console.WriteLine($"[System] Peek: {sysStack.Peek()}");    // Should be 30
    Console.WriteLine($"[System] Contains(20): {sysStack.Contains(20)}"); // True

    sysStack.Clear();
    Console.WriteLine($"\n[System] Count after Clear: {sysStack.Count}");

    try { sysStack.Peek(); }
    catch (Exception ex) { Console.WriteLine($"[System] Peek throws: {ex.Message}"); }

    try { sysStack.Pop(); }
    catch (Exception ex) { Console.WriteLine($"[System] Pop throws: {ex.Message}"); }

    // --------------------------------------------
    Console.WriteLine("\n======= CUSTOM STACK TESTS =======");
    MyStack<int> myStack = new MyStack<int>();

    Console.WriteLine("[Custom] Initial Count: " + myStack.Count);

    Console.WriteLine("\n[Custom] Pushing values: 15, 10, 20, 30, 32");
    myStack.Push(15);
    myStack.Push(10);
    myStack.Push(20);
    myStack.Push(30);
    myStack.Push(32);

    foreach (var s in myStack)
    {
        Console.WriteLine(s);
    }

    Console.WriteLine("[Custom] Count after Push: " + myStack.Count);

    Console.WriteLine("[System] Count after Push: " + sysStack.Count);
    Console.WriteLine("--------------------- [Custom] Copy to ---------------------");
    int[] arr2 = new int[7];
    myStack.CopyTo(arr2, 1);
    foreach (var s in arr2.ToArray())
    {
        Console.WriteLine(s);
    }
    Console.WriteLine("--------------------- ---------------- ---------------------");

    
    Console.WriteLine("\n[Custom] ToArray (LIFO):");
    var myArray = myStack.ToArray();
    foreach (var i in myArray)
        Console.WriteLine($"  [Custom] {i}");

    Console.WriteLine($"\n[Custom] Pop: {myStack.Pop()}");  // Should remove 32
    Console.WriteLine($"[Custom] Peek: {myStack.Peek()}");  // Should be 30
    Console.WriteLine($"[Custom] Contains(20): {myStack.Contains(20)}"); // True

    myStack.Clear();
    Console.WriteLine($"\n[Custom] Count after Clear: {myStack.Count}");

    
    try { myStack.Peek(); }
    catch (Exception ex) { Console.WriteLine($"[Custom] Peek throws: {ex.Message}"); }

    try { myStack.Pop(); }
    catch (Exception ex) { Console.WriteLine($"[Custom] Pop throws: {ex.Message}"); }

    // ================================
    Console.WriteLine("\n======= ADVANCED TESTS =======");

    // Push many to force resize
    Console.WriteLine("\n[Custom] Pushing 100 items to test Resize:");
    for (int i = 0; i < 100; i++)
        myStack.Push(i);

    Console.WriteLine($"[Custom] Count after 100 Pushes: {myStack.Count}");
    Console.WriteLine($"[Custom] Top item: {myStack.Peek()}");

    // Contains at end
    Console.WriteLine($"[Custom] Contains(0): {myStack.Contains(0)}");   // True
    Console.WriteLine($"[Custom] Contains(99): {myStack.Contains(99)}"); // True
    Console.WriteLine($"[Custom] Contains(100): {myStack.Contains(100)}"); // False

    // Pop all and check
    Console.WriteLine("\n[Custom] Popping all to test stability:");
    while (true)
    {
        try
        {
            Console.WriteLine($"  Popped: {myStack.Pop()}");
        }
        catch
        {
            Console.WriteLine("  Stack empty.");
            break;
        }
    }

    Console.WriteLine($"[Custom] Final Count: {myStack.Count}");

    // ================================
    Console.WriteLine("\n======= REFERENCE TYPE TEST =======");
    Stack<string> sysStr = new Stack<string>();
    MyStack<string> myStr = new MyStack<string>();

    sysStr.Push("apple");
    sysStr.Push("banana");

    myStr.Push("apple");
    myStr.Push("banana");

    Console.WriteLine($"[System] Pop: {sysStr.Pop()}");
    Console.WriteLine($"[Custom] Pop: {myStr.Pop()}");

    Console.WriteLine($"[System] Peek: {sysStr.Peek()}");
    Console.WriteLine($"[Custom] Peek: {myStr.Peek()}");

    Console.WriteLine($"[System] Contains(\"apple\"): {sysStr.Contains("apple")}");
    Console.WriteLine($"[Custom] Contains(\"apple\"): {myStr.Contains("apple")}");

    sysStr.Clear();
    myStr.Clear();

    Console.WriteLine($"[System] Count after Clear: {sysStr.Count}");
    Console.WriteLine($"[Custom] Count after Clear: {myStr.Count}");
    
}

}

public class MyStack<T> : IEnumerable<T>
{
    private T[] _data;
    private const int DEFAULT_SIZE = 4;

    private int _size;// To avoid default values exist in arr but actually we don't push them, It is different from _data.Length,this is actual size of stack (Pushed elements not default values, but in _data.Length also contains defaults)

    private int _index = -1; // pointer to the current item

    public MyStack(int size = DEFAULT_SIZE)
    {
        _data = new T[size];
    }

    public int Count => _size;

    // Pushes an item to the top of the stack. If it is full double the array
    // Average Case: O(1)
    // Worst Case (when full & resize): O(n)
    public void Push(T item)
    {
        if (IsFull())
        {
            Resize(ref _data, 2 * _data.Length);
        }

        _data[++_index] = item;
        _size++;
    }

    // Pops an item from the top of the stack. If the stack is empty, Pop
    // throws an InvalidOperationException.
    // Time Complexity: O(1)
    public T Pop()
    {
        if (IsEmpty()) throw new InvalidOperationException("You cannot pop from empty stack.");

        T popped = _data[_index];
        
        _data[_index] = default!;
        _index--;
        _size--;
        
        return popped;
    }

    // Returns the top object on the stack without removing it. If the stack
    // is empty, Peek throws an InvalidOperationException.
    // Time Complexity: O(1)
    public T Peek()
    {
        if (IsEmpty()) throw new InvalidOperationException("You cannot peek from empty stack.");

        return _data[_index];
    }

    // Copies the Stack to an array, in the same order Pop would return the items.
    // Time Complexity: O(N)   
    public T[] ToArray()
    {
        if (_data.Length == 0)
            return Array.Empty<T>();

        return CopyArrayInReverse(_data);
    }
    
    
    // Removes all Objects from the Stack.
    public void Clear()
    {
        ClearArr(_data, 0, _size);
      // Array.Clear(_data, 0, _data.Length);
        _index = -1;
    }

    private void ClearArr(T[] data, int start, int length)
    {
        for (int i = start + length - 1; i >= start ; i--)
        {
            data[i] = default!;
        }

        _size = 0;
        _index = -1;
    }

    // To reduce the capacity of the internal array (_array) to free unused memory when your stack has extra space that's no longer needed.
    public void TrimExcess()
    {
        int threshold = (int)(_data.Length * 0.9);// Defines a "90% usage" threshold. If you're using less than 90% of the allocated array, you are considered to have too much unused memory.
        if (_size < threshold) // if _size (actual pushed elements) count is less than threshold
        {
            Resize(ref _data, 2 * _data.Length);
        }
    }

    public bool Contains(T item)
    {
        for (int i = _size - 1; i >= 0; i--)
        {
            if (_data[i].Equals(item)) return true;
        }
        return false;
    }

    // Copies the stack into an array.
    public void CopyTo(T[] newArray, int arrayIndex) // arrayIndex specifies the starting position (index) in the destination array (array) where the copying of the stack elements should begin.
    {
        ArgumentNullException.ThrowIfNull(newArray);
        
        if (arrayIndex < 0 || arrayIndex > newArray.Length)
        {
            throw new InvalidOperationException();
        }
        if (newArray.Length - arrayIndex < _size)
        {
            throw new InvalidOperationException("Stack size is greater than array length from offset(arrayIndex).");
        }
        
        int startIndex = 0; // start
        int dstIndex = arrayIndex + _size; // end

        while (startIndex < _size) // if start less than actual size
        {
            newArray[--dstIndex] = _data[startIndex++];// Store as reverse order
            // arr[end] = stackArr[start]
            // --end ; start++
        }
    }

    /*************** Utils *********************/
    private T[] CopyArrayInReverse(T[] array)
    {
        T[] newArr = new T[_size];
        
        for (int i = 0; i < _size; i++)
        {
            newArr[i] = _data[_size - 1 - i];
        }
        return newArr;
    }
    
    // Resize logic -> copy and resize
    private void Resize(ref T[] source, int newSize)
    {

        if (newSize > source.Length)
        {
            T[] localCopy = new T[newSize];
        
            for (int i = 0; i < source.Length; i++)
            {
                localCopy[i] = source[i];
            }

            source = localCopy;
        }
        
    }
    
    private bool IsFull() => _index == _data.Length - 1;

    private bool IsEmpty() => _index == -1; // By default we have arr with length = 10, therefore we can know emptyness of stack with only index pointer

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = _size - 1; i >= 0; i--)
        {
            yield return _data[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}