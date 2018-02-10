using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsynchronousMergeSort
{
    public class Program
    {
        public static readonly int ArrayLength = 5000000;
        public static readonly int ParallelTests = 16;

        static void Main()
        {
            var array = GetArrayOfRandomNumbers(ArrayLength, int.MinValue, int.MaxValue, int.MaxValue);

            for (int i = 1; i <= ParallelTests; i++)
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                MergeSortParallel(GetArrayCopy(array), 0, ArrayLength - 1, i);
                stopWatch.Stop();

                Console.WriteLine("parallel x{0} merge sort time={1}", i, stopWatch.Elapsed.TotalMilliseconds);
            }

            Console.ReadKey();
        }

        public static int[] GetArrayCopy(int[] array)
        {
            var arrayCopy = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                arrayCopy[i] = array[i];
            }

            return arrayCopy;
        }

        public static void MergeSortParallel(int[] array, int minIndex, int maxIndex, int parallelTaskCount)
        {
            if (parallelTaskCount == 1)
                MergeSort(array, minIndex, maxIndex);
            else if (parallelTaskCount == 2)
                MergeSortParallelX2(array, minIndex, maxIndex);
            else if (parallelTaskCount == 3)
                MergeSortParallelX3(array, minIndex, maxIndex);
            else if (parallelTaskCount > 3)
            {
                int midIndex = (minIndex + maxIndex) / 2;

                if (minIndex >= maxIndex)
                    return;

                var task1 = new Task(() => MergeSortParallel(array, minIndex, midIndex, parallelTaskCount / 2));
                task1.Start();
                MergeSortParallel(array, midIndex + 1, maxIndex, parallelTaskCount - parallelTaskCount / 2);

                task1.Wait();

                Merge(array, minIndex, midIndex, maxIndex);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public static int[] GetArrayOfRandomNumbers(int count, int min, int max, int seed)
        {
            int[] numbers = new int[count];
            Random random = new Random(seed);
            for (int i = 0; i < count; i++)
            {
                numbers[i] = random.Next(min, max);
            }

            return numbers;
        }

        public static void MergeSort(int[] array, int minIndex, int maxIndex)
        {
            int midIndex = (minIndex + maxIndex) / 2;

            if (minIndex >= maxIndex)
                return;

            MergeSort(array, minIndex, midIndex);
            MergeSort(array, midIndex + 1, maxIndex);
            Merge(array, minIndex, midIndex, maxIndex);
        }

        public static void MergeSortParallelX2(int[] array, int minIndex, int maxIndex)
        {
            int midIndex = (minIndex + maxIndex) / 2;

            if (minIndex >= maxIndex)
                return;

            var task1 = new Task(() => MergeSort(array, minIndex, midIndex));
            task1.Start();
            MergeSort(array, midIndex + 1, maxIndex);

            task1.Wait();

            Merge(array, minIndex, midIndex, maxIndex);
        }

        public static void MergeSortParallelX3(int[] array, int minIndex, int maxIndex)
        {
            int midIndex = minIndex + (maxIndex - minIndex) * 2 / 3;

            if (minIndex >= maxIndex)
                return;

            var task1 = new Task(() => MergeSortParallelX2(array, minIndex, midIndex));
            task1.Start();
            MergeSort(array, midIndex + 1, maxIndex);

            task1.Wait();

            Merge(array, minIndex, midIndex, maxIndex);
        }

        public static void Merge(int[] array, int minIndex, int midIndex, int maxIndex)
        {
            int[] array1 = new int[midIndex - minIndex + 1];
            int[] array2 = new int[maxIndex - midIndex];

            for (int i = minIndex; i <= midIndex; i++)
            {
                array1[i - minIndex] = array[i];
            }
            for (int i = midIndex + 1; i <= maxIndex; i++)
            {
                array2[i - (midIndex + 1)] = array[i];
            }

            int index = minIndex;
            int index1 = 0;
            int index2 = 0;

            while (index1 < array1.Length && index2 < array2.Length)
            {
                if (array1[index1] > array2[index2])
                {
                    array[index++] = array2[index2++];
                }
                else
                {
                    array[index++] = array1[index1++];
                }
            }

            while (index2 < array2.Length)
            {
                array[index++] = array2[index2++];
            }
            while (index1 < array1.Length)
            {
                array[index++] = array1[index1++];
            }
        }
       
    }
}
