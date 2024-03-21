namespace PROGRAM
{
    public class IndexedInt(int value, int index)
    {
        public int Value { get; set; } = value;
        public int OriginalIndex { get; private set; } = index; // To keep track of the original index of the value
        public int SortedIndex { get; set; } = -1; // To keep track of the current index of the value

        public static IndexedInt[] CreateIndexedArray(int[] values)
        {
            // Create an array of IndexedValue objects
            IndexedInt[] indexedValues = new IndexedInt[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                // Assign the value and the index to the IndexedValue object
                indexedValues[i] = new IndexedInt(values[i], i);
            }
            return indexedValues;
        }

        public override string ToString() // Override the ToString method to display the value, original index, and sorted index when printed
        {
            return $"Value: {Value},\t Original Index: {OriginalIndex}" + (SortedIndex != -1 ? $",\t Sorted Index: {SortedIndex}" : "");
        }
    }

    public class TrackedArray(int[] initialData)
    {
        // Create an array of IndexedValue objects from the initial data to keep track of the original indices
        private readonly IndexedInt[] data = IndexedInt.CreateIndexedArray(initialData);

        // To keep track of the number of operations performed on the data
        public int ReadOperations { get; private set; } = 0; 
        public int WriteOperations { get; private set; } = 0;


        public IndexedInt this[int index]
        {
            get
            {
                ReadOperations++;
                return data[index]; // Return the value of the IndexedValue object at the specified index
            }
            set
            {
                WriteOperations++;
                data[index] = value; // Set the value of the IndexedValue object at the specified index
            }
        }

        public int Length => data.Length;

        public void ResetOperations()
        {
            ReadOperations = 0;
            WriteOperations = 0;
        }

        public override string ToString()
        {
            return $"Item Count: {data.Length},\t Operations: {ReadOperations + WriteOperations},\t Read Operations: {ReadOperations},\t Write Operations: {WriteOperations}";
        }
    }
}
