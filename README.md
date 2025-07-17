# 🧩 Data Structures in C# – Custom Implementations

This project contains custom implementations of core data structures in **C#**, designed from scratch to demonstrate deep understanding of data structure logic and internals.

---

## 📦 Implemented Data Structures

### ✅ Custom Queue
- FIFO (First In, First Out) behavior.
- Supports `Enqueue`, `Dequeue`, `Peek`, and internal array resizing logic, etc.
- Efficient and educational alternative to built-in `Queue<T>`.

### ✅ Custom Stack
- LIFO (Last In, First Out) behavior.
- Supports `Push`, `Pop`, `Peek`, and resizing logic, etc.
- Demonstrates classic stack behavior without relying on .NET collections.

### ✅ Custom Linked Lists

#### ➤ Singly Linked List
- Basic node-based implementation.
- Supports `AddFirst`, `AddLast`, `Remove`, `Find`, etc.

#### ➤ Doubly Linked List
- Nodes contain both `Next` and `Prev` pointers.
- Supports bidirectional traversal, insertion, and deletion.

#### ➤ Circular Doubly Linked List
- Head and tail are connected to form a circular loop.
- Each node links forward and backward to its neighbors.
- Supports full set of operations:
  - `AddFirst`, `AddLast`, `AddBefore`, `AddAfter`
  - `RemoveFirst`, `RemoveLast`, `Remove`
  - `Find`, `FindLast`, `Insert(index)`
  - `CopyTo(array, index)`, `Clear()`
  - Full iteration with `IEnumerable<T>`

---

## 🔍 Purpose

These implementations are designed for:
- Practicing algorithm fundamentals.
- Understanding memory, pointer behavior, and linked structure manipulation.
- Preparing for technical interviews.
- Gaining full control over data structure internals.
