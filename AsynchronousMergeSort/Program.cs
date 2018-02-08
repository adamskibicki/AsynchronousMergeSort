using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsynchronousMergeSort
{
    public class Program
    {
        static void Main()
        {
            var array = GetArrayOfRandomNumbers(10000000, int.MinValue, int.MaxValue);
            var arrayCopy2 = new int[10000000];
            var arrayCopy3 = new int[10000000];
            var arrayCopy4 = new int[10000000];
            var arrayCopy6 = new int[10000000];
            var arrayCopy8 = new int[10000000];


            for (int i = 0; i < 10000000; i++)
            {
                arrayCopy2[i] = array[i];
                arrayCopy3[i] = array[i];
                arrayCopy4[i] = array[i];
                arrayCopy6[i] = array[i];
                arrayCopy8[i] = array[i];
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            MergeSort(array, 0, 10000000 - 1);
            stopWatch.Stop();

            Console.WriteLine("merge sort time={0}", stopWatch.Elapsed.TotalMilliseconds);



            stopWatch = new Stopwatch();
            stopWatch.Start();
            MergeSortParallelX2(arrayCopy2, 0, 10000000 - 1);
            stopWatch.Stop();

            Console.WriteLine("parallel x2 merge sort time={0}", stopWatch.Elapsed.TotalMilliseconds);

            stopWatch = new Stopwatch();
            stopWatch.Start();
            MergeSortParallelX4(arrayCopy4, 0, 10000000 - 1);
            stopWatch.Stop();

            Console.WriteLine("parallel x4 merge sort time={0}", stopWatch.Elapsed.TotalMilliseconds);

            stopWatch = new Stopwatch();
            stopWatch.Start();
            MergeSortParallelX3(arrayCopy3, 0, 10000000 - 1);
            stopWatch.Stop();

            Console.WriteLine("parallel x3 merge sort time={0}", stopWatch.Elapsed.TotalMilliseconds);

            stopWatch = new Stopwatch();
            stopWatch.Start();
            MergeSortParallelX6(arrayCopy6, 0, 10000000 - 1);
            stopWatch.Stop();

            Console.WriteLine("parallel x6 merge sort time={0}", stopWatch.Elapsed.TotalMilliseconds);

            stopWatch = new Stopwatch();
            stopWatch.Start();
            MergeSortParallelX8(arrayCopy8, 0, 10000000 - 1);
            stopWatch.Stop();

            Console.WriteLine("parallel x8 merge sort time={0}", stopWatch.Elapsed.TotalMilliseconds);

            Console.ReadKey();
        }

        public static int[] GetArrayOfRandomNumbers(int count, int min, int max)
        {
            int[] numbers = new int[count];
            Random random = new Random();
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

        public static void MergeSortParallelX4(int[] array, int minIndex, int maxIndex)
        {
            int midIndex = (minIndex + maxIndex) / 2;

            if (minIndex >= maxIndex)
                return;

            var task1 = new Task(() => MergeSortParallelX2(array, minIndex, midIndex));
            task1.Start();
            MergeSortParallelX2(array, midIndex + 1, maxIndex);

            task1.Wait();

            Merge(array, minIndex, midIndex, maxIndex);
        }

        public static void MergeSortParallelX3(int[] array, int minIndex, int maxIndex)
        {
            int midIndex = (minIndex + maxIndex) * 2 / 3;

            if (minIndex >= maxIndex)
                return;

            var task1 = new Task(() => MergeSortParallelX2(array, minIndex, midIndex));
            task1.Start();
            MergeSort(array, midIndex + 1, maxIndex);

            task1.Wait();

            Merge(array, minIndex, midIndex, maxIndex);
        }

        public static void MergeSortParallelX8(int[] array, int minIndex, int maxIndex)
        {
            int midIndex = (minIndex + maxIndex) / 2;

            if (minIndex >= maxIndex)
                return;

            var task1 = new Task(() => MergeSortParallelX4(array, minIndex, midIndex));
            task1.Start();
            MergeSortParallelX4(array, midIndex + 1, maxIndex);

            task1.Wait();

            Merge(array, minIndex, midIndex, maxIndex);
        }

        public static void MergeSortParallelX6(int[] array, int minIndex, int maxIndex)
        {
            int midIndex = (minIndex + maxIndex) / 2;

            if (minIndex >= maxIndex)
                return;

            var task1 = new Task(() => MergeSortParallelX3(array, minIndex, midIndex));
            task1.Start();
            MergeSortParallelX3(array, midIndex + 1, maxIndex);

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

        public static int[] MergeArrays(int[] array1, int[] array2)
        {
            var array = new int[array1.Length + array2.Length];

            int index = 0;
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

            return array;
        }
    }
}
