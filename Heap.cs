using System;
using System.Text;

/// <summary>
/// Array-based implementation of heap. Use a fixed array size. Knowing the amount of elements
/// by advance is recommended. 
/// </summary>
class Heap{
    /// <summary>
    /// The amount of elements of the heap.
    /// </summary>
    public int Size {get; private set;}
    /// <summary>
    /// The maximum amount of elements of the heap.
    /// It holds that Size <= Capacity
    /// </summary>
    public int Capacity {get; private set;}
    /// <summary>
    /// The heap elements.
    /// </summary>
    private int[] node;

    public Heap(int capacity){
        if(capacity < 0)throw new ArgumentException("capacity must be non-negative");
        Capacity = capacity;
        Size = 0;
        node = new int[Capacity];
    }
    /// <summary>
    /// Builds a heap based on a given array.
    /// </summary>
    /// <param name="baseArray"></param>
    public Heap(int[] baseArray){
        Capacity = baseArray.Length;
        Size = Capacity;
        node = new int[Capacity];
        for(int i=0; i < Size;++i) node[i] = baseArray[i];
        for(int i = (Size / 2) - 1; i >= 0;--i) HeapifyDown(i);
    }
    /// <summary>
    /// Insert an element on the heap.
    /// </summary>
    /// <param name="value"></param>
    public void Insert(int value){
        if(Size == Capacity)throw new InvalidOperationException("Inserted at heap with full capacity");

        //Look for the next position to insert
        ++Size;
        node[Size - 1] = value;
        //Heapify to mantain invariants after insertion.
        HeapifyUp(Size - 1);
    }
    /// <summary>
    /// Retrieves the minimum element from the heap but does not removes it.
    /// </summary>
    /// <returns>The minimum element on the heap.</returns>
    public int PeekMinimum(){
        if(Size == 0)throw new InvalidOperationException("Cannot retrieve minimum on empty heap.");
        return node[0];
    }
    /// <summary>
    /// Retrieves the minimum element from the heap and removes it.
    /// </summary>
    /// <returns>The minimum element on the heap.</returns>
    public int RemoveMinimum(){
        int minimum = PeekMinimum();

        node[0] = node[--Size];// Assign the value of the last element of the heap to the root

        HeapifyDown(0);

        return minimum;
    }
    /// <summary>
    /// Push down (toward the leaves) an element on the heap while mantaining invariants.
    /// </summary>
    /// <param name="actualIndex">The index of the node to heapify.</param>
    private void HeapifyDown(int actualIndex){
        int c = node[actualIndex];//for swapping

        // While node is not a leaf node
        while(actualIndex < (Size >> 1)){
            // Compute child nodes indexes.
            int leftChildIndex = (actualIndex << 1) + 1;
            int rightChildIndex = leftChildIndex + 1;
            if(rightChildIndex >= Size)rightChildIndex = leftChildIndex;// special case , avoid overflow

            // Consider the minimum between the actual node and its children nodes.

            //The minimum is the left child
            if(node[leftChildIndex] <= node[actualIndex] && node[leftChildIndex] <= node[rightChildIndex]){
                //Swap actual node and left child values
                node[actualIndex] = node[leftChildIndex];
                node[leftChildIndex] = c;

                actualIndex = leftChildIndex;
            }
            //The minimum if the right child
            else if(node[rightChildIndex] <= node[actualIndex] && node[rightChildIndex] <= node[leftChildIndex]){
                //Swap actual node and left child values
                node[actualIndex] = node[rightChildIndex];
                node[rightChildIndex] = c;

                actualIndex = rightChildIndex;
            }
            //The minimum is the actual node ,invariants hold, stop
            else return;
        }
    }

    /// <summary>
    /// Push up (toward the root) an element on the heap while mantaining heap invariants.
    /// </summary>
    /// <param name="actualIndex">The index of the node to heapify.</param>
    private void HeapifyUp(int actualIndex){
        //To store the parent of the actual index
        int parentIndex;

        //While not in root
        while(actualIndex > 0){
            //Calculate parent index
            if(actualIndex % 2 == 0)parentIndex = (actualIndex - 1) / 2;
            else parentIndex = actualIndex / 2;

            //Check if the ordering holds
            if(node[actualIndex] <= node[parentIndex]){
                //Ordering does not hold, swap actual node and parent node.
                int c = node[actualIndex];
                node[actualIndex] = node[parentIndex];
                node[parentIndex] = c;
            }
            else{
                //Ordering holds, stop.
                return;
            }
        }
    }

    public override string ToString(){
        StringBuilder s = new StringBuilder();
        s.Append('[');
        for(int i=0;i<Size;++i){
            s.Append(" " + node[i].ToString() + " ");
        }
        s.Append(']');
        return s.ToString();
    }
}