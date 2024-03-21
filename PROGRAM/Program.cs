using static PROGRAM.Algorithms;

namespace PROGRAM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ask user for a network to analyze.
            Console.WriteLine("Which network would you like to analyze?");
            string network = Input.GetChoiceFromUser(["1", "2", "3"], "(1-3)");
            Console.WriteLine("Which mode would you like to analyze the network in?");
            string mode = Input.GetChoiceFromUser(["1", "2"], "1. 256\n2. 2048\n(1-2)") == "1" ? "256" : "2048";

            // Load data from file depending on the network and mode.
            int[] rawData = Input.ReadIntFile($"NetworkData/Net_{network}_{mode}.txt");
            Console.WriteLine("Data loaded successfully, would you like to merge it with another network's data?");

            // Merge data with another network if desired.
            if (Input.GetChoiceFromUser(["1", "2"], "1. Yes\n2. No\n(1-2)") == "1")
            {
                Console.WriteLine("Which network would you like to merge with?");
                string mergeNetwork = Input.GetChoiceFromUser(["1", "2", "3"], "(1-3)");
                Console.WriteLine("Which mode would you like to merge the network in?");
                string mergeMode = Input.GetChoiceFromUser(["1", "2"], "1. 256\n2. 2048\n(1-2)") == "1" ? "256" : "2048";
                if (mergeMode == "2048") mode = "2048"; // If merging with 2048 mode, set the mode to 2048 to display every 50th value
                int[] mergeData = Input.ReadIntFile($"NetworkData/Net_{mergeNetwork}_{mergeMode}.txt");
                rawData = [.. rawData, .. mergeData];
                Console.WriteLine("Data merged successfully.");
            }

            // Create a TrackedArray object to keep track of operations on the data
            TrackedArray data = new(rawData);

            // Ask user for sorting algorithm and direction.
            Console.WriteLine("What sorting algorithm would you like to use?");
            string sortingAlgorithm = Input.GetChoiceFromUser(["1", "2", "3"], "1. Bubble sort\n2. Quick sort\n3. Insertion sort\n(1-3)");
            Console.WriteLine("How would you like to display the data?");
            string direction = Input.GetChoiceFromUser(["1", "2"], "1. Ascending\n2. Decending\n(1-2)");
            Console.WriteLine("Sorting data...");

            // Sort the data using the selected algorithm and display the sorted data.
            data.ResetOperations(); // Reset the operation count before sorting
            TrackedArray sortedData;
            sortedData = sortingAlgorithm switch
            {
                "1" => BubbleSort(data),
                "2" => QuickSort(data),
                "3" => InsertionSort(data),
                _ => throw new Exception("Invalid sorting algorithm")
            };

            Console.WriteLine("Displaying sorted data (Indexing starts at 0)...");

            string operations = sortedData.ToString(); // to ignore the display operations, we can store the operations before.

            // Display the sorted data in the selected direction
            int start = direction == "1" ? 0 : sortedData.Length - 1;
            int end = direction == "1" ? sortedData.Length : -1;
            int step = direction == "1" ? 10 : -10;
            if (mode == "2048") step *= 5; // Increase step size for 2048 mode

            for (int i = start; direction == "1" ? i < end : i > end; i += step)
            {
                sortedData[i].SortedIndex = i;
                Console.WriteLine(sortedData[i]);
            }

            Console.WriteLine("Data sorted successfully.");
            Console.WriteLine(operations);

            // Ask user if they would like to search for a specific value in the data.
            Console.WriteLine("Would you like to search for a specific value in the data?");
            if (Input.GetChoiceFromUser(["1", "2"], "1. Yes\n2. No\n(1-2)") == "2") return;

            // Ask user for search type and value to search for.
            Console.WriteLine("What search algorithm would you like to use?");
            string searchType = Input.GetChoiceFromUser(["1", "2"], "1. Linear search\n2. Binary search\n(1-2)");
            int value = Input.GetIntFromUser("Value of datapoint");
            Console.WriteLine("Locating...");

            data.ResetOperations();
            Tuple<IndexedInt[], bool> dataPoints = searchType switch
            {
                "1" => LinearSearch(data, value),
                "2" => BinarySearch(data, value),
                _ => throw new Exception("Invalid search type")
            };

            if (dataPoints.Item2)
            {
                Console.WriteLine($"Found {dataPoints.Item1.Length} instances of \"{value}\" in the data:");
                Array.ForEach(dataPoints.Item1, Console.WriteLine);
            }
            else
            {
                Console.WriteLine($"Could not find \"{value}\" in the data.");
                Console.WriteLine($"Found closest value from \"{value}\" in the data:");
                Console.WriteLine(dataPoints.Item1[0]);
            }
            Console.WriteLine(data);

        }
    }
}
