using LinkedList;

public class Startup
{
    static void Main()
    {
        LinkedList<int> sysLinkedList = new LinkedList<int>();
        
        
        // ----------------- Singly Linked List -----------------
        Console.WriteLine("----------------- Singly Linked List -----------------");
        SinglyLinkedList<int> mySinglyLinkedList = new SinglyLinkedList<int>();

        // Initial inserts
        mySinglyLinkedList.AddFirst(1);   // List: 1
        mySinglyLinkedList.AddFirst(2);   // List: 2 → 1 
        mySinglyLinkedList.AddFirst(3);   // List: 3 → 2 → 1
        mySinglyLinkedList.AddLast(5);    // List: 3 → 2 → 1 → 5
        mySinglyLinkedList.AddLast(6);    // List: 3 → 2 → 1 → 5 → 6
        mySinglyLinkedList.AddLast(7);    // List: 3 → 2 → 1 → 5 → 6 → 7

        // Insert 15 at different positions
        mySinglyLinkedList.Insert(15, 0);  // Insert at head
        mySinglyLinkedList.Insert(15, 3);  // Insert in middle
        mySinglyLinkedList.Insert(15, 6);  // Insert in middle

        Console.WriteLine("Initial list:");
        mySinglyLinkedList.Display();
        Console.WriteLine();

        // Test RemoveFirst
        Console.WriteLine("Removing first:");
        mySinglyLinkedList.RemoveFirst();
        mySinglyLinkedList.Display();
        Console.WriteLine();

        // Test RemoveLast
        Console.WriteLine("Removing last:");
        mySinglyLinkedList.RemoveLast();
        mySinglyLinkedList.Display();
        Console.WriteLine();

        // Test Remove(value): value exists multiple times
        Console.WriteLine("Removing first occurrence of 15:");
        mySinglyLinkedList.Remove(15);
        mySinglyLinkedList.Display();
        Console.WriteLine();

        // Test Remove(value): value not in list
        Console.WriteLine("Trying to remove 100 (not in list):");
        bool removed = mySinglyLinkedList.Remove(100);
        Console.WriteLine($"Remove(100) returned: {removed}");
        mySinglyLinkedList.Display();
        Console.WriteLine();
        
        // ----------------- Non-Circular Doubly Linked List -----------------
        Console.WriteLine("----------------- Non-Circular Doubly Linked List -----------------");
        var list = new DoublyLinkedList<int>();

        Console.WriteLine("== AddFirst Tests ==");
        list.AddFirst(10); // List: 10
        list.AddFirst(20); // List: 20 -> 10
        list.AddFirst(30); // List: 30 -> 20 -> 10
        list.Display();
        Console.WriteLine();

        Console.WriteLine("== AddLast Tests ==");
        list.AddLast(40); // List: 30 -> 20 -> 10 -> 40
        list.AddLast(50); // List: 30 -> 20 -> 10 -> 40 -> 50
        list.Display();
        Console.WriteLine();

        Console.WriteLine("== Insert at Head (index 0) ==");
        list.Insert(5, 0); // List: 5 -> 30 -> 20 -> 10 -> 40 -> 50
        list.Display();
        Console.WriteLine();

        Console.WriteLine("== Insert at Middle (index 3) ==");
        list.Insert(15, 3); // List: 5 -> 30 -> 20 -> 15 -> 10 -> 40 -> 50
        list.Display();
        Console.WriteLine();

        Console.WriteLine("== Insert at End (index = size) ==");
        list.Insert(60, 7); // List: 5 -> 30 -> 20 -> 15 -> 10 -> 40 -> 50 -> 60
        list.Display();
        Console.WriteLine();

        Console.WriteLine("== Find() Test: Find 15 ==");
        var foundNode = list.Find(15);
        Console.WriteLine(foundNode != null 
            ? $"Found Node: {foundNode.Value}" 
            : "Value not found");
        Console.WriteLine();

        Console.WriteLine("== Find() Test: Find 100 ==");
        var notFound = list.Find(100);
        Console.WriteLine(notFound != null 
            ? $"Found Node: {notFound.Value}" 
            : "Value not found");
        Console.WriteLine();
        
        // ----------------- Circular Doubly Linked List -----------------
        Console.WriteLine("----------------- Circular Doubly Linked List -----------------");

        var cDLinkedList = new CircularDoublyLinkedList<int>();

        // 1. Test AddFirst()
        cDLinkedList.AddFirst(1);
        // Expected: 1
        cDLinkedList.Display(); // Output: 1 -> END / 1 -> START

        // 2. AddAfter()
        cDLinkedList.AddAfter(cDLinkedList.Find(1)!, 2);
        // Expected: 1 -> 2
        cDLinkedList.Display(); // Output: 1 -> 2 -> END / 2 -> 1 -> START

        // 3. AddLast()
        cDLinkedList.AddLast(3);
        // Expected: 1 -> 2 -> 3
        cDLinkedList.Display(); // Output: 1 -> 2 -> 3 -> END / 3 -> 2 -> 1 -> START

        // 4. AddBefore()
        cDLinkedList.AddBefore(cDLinkedList.Find(3)!, 99);
        // Expected: 1 -> 2 -> 99 -> 3
        cDLinkedList.Display(); // Output: 1 -> 2 -> 99 -> 3 -> END / 3 -> 99 -> 2 -> 1 -> START

        // 5. Test Contains()
        Console.WriteLine("Contains 99? " + cDLinkedList.Contains(99)); // true
        Console.WriteLine("Contains 100? " + cDLinkedList.Contains(100)); // false

        // 6. Find & FindLast
        Console.WriteLine("Find(99): " + cDLinkedList.Find(99)?.Value); // 99
        Console.WriteLine("FindLast(2): " + cDLinkedList.FindLast(2)?.Value); // 2

        // 7. Insert at position
        cDLinkedList.Insert(55, 2);
        // Expected: 1 -> 2 -> 55 -> 99 -> 3
        cDLinkedList.Display();

        // 8. Remove by value
        cDLinkedList.Remove(2);
        // Expected: 1 -> 55 -> 99 -> 3
        cDLinkedList.Display();

        // 9. RemoveFirst
        cDLinkedList.RemoveFirst(); // Removes 1
        cDLinkedList.Display(); // Expected: 55 -> 99 -> 3

        // 10. RemoveLast
        cDLinkedList.RemoveLast(); // Removes 3
        cDLinkedList.Display(); // Expected: 55 -> 99

        // 11. Remove by Node
        var node99 = cDLinkedList.Find(99)!;
        cDLinkedList.Remove(node99);
        cDLinkedList.Display(); // Expected: 55

        // 12. Enumerator / foreach test
        Console.WriteLine("Foreach loop:");
        foreach (var node in cDLinkedList)
            Console.Write(node?.Value + " "); // Output: 55

        // 13. CopyTo
        var array = new int[10];
        cDLinkedList.CopyTo(array, 3);
        Console.WriteLine("\nCopied to array[3]: " + string.Join(", ", array)); 
        // Expected: 0,0,0,55,0,0,0,0,0,0

        // 14. Clear test
        cDLinkedList.Clear();
        Console.WriteLine("After clear:");
        cDLinkedList.Display(); // Expected: []

        // 15. Edge case: AddAfter on single element
        cDLinkedList.AddFirst(100);
        cDLinkedList.AddAfter(cDLinkedList.Find(100)!, 200);
        cDLinkedList.Display(); // Expected: 100 -> 200

        // 16. AddBefore on first element
        cDLinkedList.AddBefore(cDLinkedList.Find(100)!, 50);
        cDLinkedList.Display(); // Expected: 50 -> 100 -> 200

        // 17. Insert at head
        cDLinkedList.Insert(10, 0);
        cDLinkedList.Display(); // 10 -> 50 -> 100 -> 200

        // 18. Insert at end
        cDLinkedList.Insert(999, cDLinkedList.Length);
        cDLinkedList.Display(); // Ends with 999

        // 19. Invalid CopyTo
        try
        {
            cDLinkedList.CopyTo(new int[1], 0);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Expected CopyTo exception: " + ex.Message); // should throw
        }

        // 20. Invalid index
        try
        {
            cDLinkedList.Insert(5, -1);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Expected invalid index: " + ex.Message);
        }

        // 21. Remove on empty list
        cDLinkedList.Clear();
        try
        {
            cDLinkedList.RemoveFirst();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Expected error on RemoveFirst: " + ex.Message);
        }

        
    }
}

