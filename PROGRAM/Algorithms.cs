using static PROGRAM.Algorithms;

namespace PROGRAM
{
    internal class Algorithms
    {
        public record DataPoint(int Value, int Index);

        internal static TrackedArray BubbleSort(TrackedArray data)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                for (int j = 0; j < data.Length - i - 1; j++)
                {
                    if (data[j].Value > data[j + 1].Value)
                    {
                        IndexedInt temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                }
            }
            return data;
        }

        internal static TrackedArray QuickSort(TrackedArray data)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < data.Length; j++)
                {
                    if (data[j].Value < data[minIndex].Value)
                    {
                        minIndex = j;
                    }
                }
                IndexedInt temp = data[minIndex];
                data[minIndex] = data[i];
                data[i] = temp;
            }
            return data;
        }

        internal static TrackedArray InsertionSort(TrackedArray data)
        {
            for (int i = 1; i < data.Length; i++)
            {
                IndexedInt key = data[i];
                int j = i - 1;
                for (; j >= 0 && data[j].Value > key.Value; j--)
                {
                    data[j + 1] = data[j];
                }
                data[j + 1] = key;
            }
            return data;
        }

        internal static Tuple<IndexedInt[], bool> LinearSearch(TrackedArray data, int value)
        {
            // locate all the indices of the value in the data using linear search.
            List<IndexedInt> dataPoints = [];
            int index = 0;
            for (; index < data.Length; index++)
            {
                if (data[index].Value == value)
                {
                    int j = index + 1;
                    for (; j < data.Length && data[j].Value == value; j++)
                    {
                        dataPoints.Add(data[j]);
                    }
                }
            }

            bool matchFound = dataPoints.Count >= 0;
            // find the nearest value if no value has been found.
            if (matchFound)
            {
                
                int minDiff = int.MaxValue; // set to the maximum value of int
                int nearestIndex = -1;
                for (int i = 0; i < data.Length; i++) // Linearly go over every value until best match is found
                {
                    int diff = Math.Abs(data[i].Value - value);
                    if (diff < minDiff) // if the difference is smaller than the previous one, update the nearest value
                    {
                        minDiff = diff;
                        nearestIndex = i;
                    }
                }
                dataPoints.Add(data[nearestIndex]);
                index = nearestIndex;
            }

            return new Tuple<IndexedInt[], bool>([.. dataPoints], matchFound);

        }

        internal static Tuple<IndexedInt[], bool> BinarySearch(TrackedArray data, int value)
        {
            // locate all the indices of the value in the data or the nearest value using binary search
            List<IndexedInt> dataPoints = [];

            int left = 0;
            int right = data.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (data[mid].Value == value)
                {
                    dataPoints.Add(data[mid]);
                    int i = mid - 1;
                    while (i >= 0 && data[i].Value == value)
                    {
                        dataPoints.Add(data[i]);
                        i--;
                    }
                    i = mid + 1;
                    while (i < data.Length && data[i].Value == value)
                    {
                        Console.WriteLine(data[i].SortedIndex);
                        dataPoints.Add(data[i]);
                        i++;
                    }
                    return new Tuple<IndexedInt[], bool>([.. dataPoints], true);
                }
                else if (data[mid].Value < value)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            bool matchFound = dataPoints.Count > 0;
            // find the nearest value using binary search if no value has been found
            if (!matchFound)
            {
                int nearestIndex = -1;
                int minDiff = int.MaxValue;
                left = 0;
                right = data.Length - 1;
                while (left <= right)
                {
                    int mid = left + (right - left) / 2;
                    int diff = Math.Abs(data[mid].Value - value);
                    if (diff < minDiff)
                    {
                        minDiff = diff;
                        nearestIndex = mid;
                    }
                    if (data[mid].Value < value)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }
                dataPoints.Add(data[nearestIndex]);
            }

            return new Tuple<IndexedInt[], bool>([.. dataPoints], matchFound);
        }
    }
}
