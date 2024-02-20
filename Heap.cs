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

        node[0] = node[Size - 1];// Assign the value of the last element of the heap to the root
        --Size;// Remove the last node from the heap

        HeapifyDown(0);

        return minimum;
    }
    /// <summary>
    /// Push down (toward the leaves) an element on the heap while mantaining invariants.
    /// </summary>
    /// <param name="actualIndex">The index of the node to heapify.</param>
    private void HeapifyDown(int actualIndex){
        // While node is not a leaf node
        while(actualIndex < Size / 2){
            // Compute child nodes indexes.
            int leftChildIndex = actualIndex * 2 + 1;
            int RightChildIndex = leftChildIndex + 1;

            // Consider the minimum between the actual node and its children nodes.

            int c = node[actualIndex];//for swapping
            if(RightChildIndex < Size){
                //The minimum is the left child
                if(node[leftChildIndex] <= node[actualIndex] && node[leftChildIndex] <= node[RightChildIndex]){
                    //Swap actual node and left child values
                    node[actualIndex] = node[leftChildIndex];
                    node[leftChildIndex] = c;

                    actualIndex = leftChildIndex;
                }
                //The minimum if the right child
                else if(node[RightChildIndex] <= node[actualIndex] && node[RightChildIndex] <= node[leftChildIndex]){
                    //Swap actual node and left child values
                    node[actualIndex] = node[RightChildIndex];
                    node[RightChildIndex] = c;

                    actualIndex = RightChildIndex;
                }
                //The minimum is the actual node ,invariants hold, stop
                else return;
            }
            //Special case when the node doesnt have right child.
            else if(node[leftChildIndex] <= node[actualIndex]){
                //Swap actual node and left child values
                node[actualIndex] = node[leftChildIndex];
                node[leftChildIndex] = c;

                actualIndex = leftChildIndex;
            }
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
}