using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AsynchronousMergeSort;

namespace AsynchronousMergeSortTests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void test_count_of_GetArrayOfRandomNumbers()
        {
            var array = Program.GetArrayOfRandomNumbers(10000, 0, 1);

            Assert.AreEqual(10000, array.Length);
        }

        [TestMethod]
        public void test_value_setting_of_GetArrayOfRandomNumbers()
        {
            var array = Program.GetArrayOfRandomNumbers(10000, 1, int.MaxValue);

            Assert.IsTrue(array.All(x => x != 0));
        }

        [TestMethod]
        public void test_MergeArrays_returns_sorted_array()
        {
            var array1 = new[] { 1, 1, 2, 2, 3, 4, 5, 8, 9 };
            var array2 = new[] { 1, 1, 2, 2, 3, 4, 5, 8, 9 };

            var array = Program.MergeArrays(array1, array2);

            var array3 = new[] { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 4, 4, 5, 5, 8, 8, 9, 9 };

            CollectionAssert.AreEqual(array3, array);
        }

        [TestMethod]
        public void test_Merge_returns_sorted_array()
        {
            var array = new[] { 1, 1, 2, 2, 3, 4, 5, 8, 9, 1, 1, 2, 2, 3, 4, 5, 8, 9 };

            Program.Merge(array, 0, 8, 17);

            var array3 = new[] { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 4, 4, 5, 5, 8, 8, 9, 9 };

            CollectionAssert.AreEqual(array3, array);
        }

        [TestMethod]
        public void test_MergeSort_returns_sorted_array()
        {
            var array = new[] { 1, 5, 8, 9, 1, 2, 2, 3, 2, 3, 4, 4, 1, 1, 2, 5, 8, 9 };

            Program.MergeSort(array, 0, 17);

            var array3 = new[] { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 4, 4, 5, 5, 8, 8, 9, 9 };

            CollectionAssert.AreEqual(array3, array);
        }

        [TestMethod]
        public void test_MergeSort()
        {
            var array = Program.GetArrayOfRandomNumbers(1000000, int.MinValue, int.MaxValue);
            var arrayCopy = new int[1000000];

            for (int i = 0; i < 1000000; i++)
            {
                arrayCopy[i] = array[i];
            }

            Program.MergeSort(array, 0, 999999);
            var list = arrayCopy.ToList();
            list.Sort();
            arrayCopy = list.ToArray();

            CollectionAssert.AreEqual(arrayCopy, array);
        }

        [TestMethod]
        public void test_MergeSortParallelX2()
        {
            var array = Program.GetArrayOfRandomNumbers(1000000, int.MinValue, int.MaxValue);
            var arrayCopy = new int[1000000];

            for (int i = 0; i < 1000000; i++)
            {
                arrayCopy[i] = array[i];
            }

            Program.MergeSortParallelX2(array, 0, 999999);
            var list = arrayCopy.ToList();
            list.Sort();
            arrayCopy = list.ToArray();

            CollectionAssert.AreEqual(arrayCopy, array);
        }

        [TestMethod]
        public void test_MergeSortParallelX4()
        {
            var array = Program.GetArrayOfRandomNumbers(1000000, int.MinValue, int.MaxValue);
            var arrayCopy = new int[1000000];

            for (int i = 0; i < 1000000; i++)
            {
                arrayCopy[i] = array[i];
            }

            Program.MergeSortParallelX4(array, 0, 999999);
            var list = arrayCopy.ToList();
            list.Sort();
            arrayCopy = list.ToArray();

            CollectionAssert.AreEqual(arrayCopy, array);
        }

        [TestMethod]
        public void test_MergeSortParallelX3()
        {
            var array = Program.GetArrayOfRandomNumbers(1000000, int.MinValue, int.MaxValue);
            var arrayCopy = new int[1000000];

            for (int i = 0; i < 1000000; i++)
            {
                arrayCopy[i] = array[i];
            }

            Program.MergeSortParallelX3(array, 0, 999999);
            var list = arrayCopy.ToList();
            list.Sort();
            arrayCopy = list.ToArray();

            CollectionAssert.AreEqual(arrayCopy, array);
        }

        [TestMethod]
        public void test_MergeSortParallelX6()
        {
            var array = Program.GetArrayOfRandomNumbers(1000000, int.MinValue, int.MaxValue);
            var arrayCopy = new int[1000000];

            for (int i = 0; i < 1000000; i++)
            {
                arrayCopy[i] = array[i];
            }

            Program.MergeSortParallelX6(array, 0, 999999);
            var list = arrayCopy.ToList();
            list.Sort();
            arrayCopy = list.ToArray();

            CollectionAssert.AreEqual(arrayCopy, array);
        }

        [TestMethod]
        public void test_MergeSortParallelX8()
        {
            var array = Program.GetArrayOfRandomNumbers(1000000, int.MinValue, int.MaxValue);
            var arrayCopy = new int[1000000];

            for (int i = 0; i < 1000000; i++)
            {
                arrayCopy[i] = array[i];
            }

            Program.MergeSortParallelX8(array, 0, 999999);
            var list = arrayCopy.ToList();
            list.Sort();
            arrayCopy = list.ToArray();

            CollectionAssert.AreEqual(arrayCopy, array);
        }
    }
}
