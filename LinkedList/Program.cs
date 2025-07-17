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
        CircularDoublyLinkedList<int> circularDoubly = new CircularDoublyLinkedList<int>();
        circularDoubly.AddFirst(1);
        circularDoubly.AddAfter(circularDoubly.Find(1)!,2);
        circularDoubly.AddLast(23);
        circularDoubly.AddBefore(circularDoubly.Find(23)!,232);
        
        circularDoubly.Display();
        
        circularDoubly.Clear();
        Console.WriteLine("\nDisplay after clear:");
        circularDoubly.Display();
        
    }
}

