//Intended for standalone usage
class Program{
    static void Main(string[] args){
        //Writing tests for the Heap class

        Console.WriteLine("Number of sessions >: ");
        int sessions = int.Parse(Console.ReadLine());
        Console.WriteLine("Array Length");
        int arrayLength = int.Parse(Console.ReadLine());

        int totalTimeNativeSort = 0;
        int totalTimeHeapSort = 0;
        int time = 0;

        while(sessions-- > 0){
            int[] array = GenerateArray(arrayLength);

            int[] original = new int[array.Length];
            for(int i=0;i<original.Length;++i)original[i] = array[i];
            
            Heap heap = new Heap(array);
            int[] sorted = new int[array.Length];

            time = Environment.TickCount;
            for(int i=0;i<sorted.Length;++i){
                sorted[i] = heap.RemoveMinimum();
            }
            totalTimeHeapSort += Environment.TickCount - time;

            time = Environment.TickCount;
            Array.Sort(array);
            totalTimeNativeSort += Environment.TickCount - time;
            
            bool fail = false;
            for(int i=0;i<array.Length;++i){
                if(sorted[i] != array[i]){
                    Console.WriteLine("error");
                    fail = true;
                    break;
                }
            }

            if(fail){
                Print(original);
                Console.WriteLine(heap);
                Print(sorted);
                Print(array);
            }
        }

        Console.WriteLine("Total time spent sorting with native functions: " + totalTimeNativeSort / 1000.0);
        Console.WriteLine("Total time spent sorting with heap sort: " + totalTimeHeapSort / 1000.0);
    }

    //Generate random array
    static private int[] GenerateArray(int length){
        Random r = new Random();
        int[] array = new int[length];
        for(int i=0;i<length;++i)array[i] = r.Next();
        return array;
    }
    static void Print(int[] array){
        Console.Write("[");
        for(int i=0;i<array.Length;++i)Console.Write(" " + array[i] + " ");
        Console.WriteLine("]");
    }
}
