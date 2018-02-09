using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AsynchronousMergeSort;

namespace AsynchronousMergeSortTests
{
    [TestClass]
    public class ProgramTests
    {
        private static void MergeSortParallelTestWrapper(int parallelTaskCount)
        {
            var array = Program.GetArrayOfRandomNumbers(100000, int.MinValue, int.MaxValue, int.MaxValue);
            var arrayCopy = Program.GetArrayCopy(array).ToList();
            arrayCopy.Sort();

            Program.MergeSortParallel(array, 0, 100000 - 1, parallelTaskCount);

            CollectionAssert.AreEqual(arrayCopy, array);
        }

        [TestMethod]
        public void test_GetArrayOfRandomNumbers_array_length()
        {
            var array = Program.GetArrayOfRandomNumbers(10000, 0, 1, int.MaxValue);

            Assert.AreEqual(10000, array.Length);
        }

        [TestMethod]
        public void test_GetArrayOfRandomNumbers_value_setting()
        {
            var array = Program.GetArrayOfRandomNumbers(10000, 1, int.MaxValue, int.MaxValue);

            Assert.IsTrue(array.All(x => x != 0));
        }

        [TestMethod]
        public void test_GetArrayOfRandomNumbers_returns_identical_arrays_for_same_seed()
        {
            var array = Program.GetArrayOfRandomNumbers(10000, 1, int.MaxValue, int.MaxValue);
            var array1 = Program.GetArrayOfRandomNumbers(10000, 1, int.MaxValue, int.MaxValue);

            CollectionAssert.AreEqual(array, array1);
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
        public void test_GetArrayCopy_returns_new_array()
        {
            var array = Program.GetArrayOfRandomNumbers(1000000, int.MinValue, int.MaxValue, int.MaxValue);
            var arrayCopy = Program.GetArrayCopy(array);

            Assert.AreNotEqual(array, arrayCopy);
        }

        [TestMethod]
        public void test_GetArrayCopy_returns_array_with_identical_values()
        {
            var array = Program.GetArrayOfRandomNumbers(1000000, int.MinValue, int.MaxValue, int.MaxValue);
            var arrayCopy = Program.GetArrayCopy(array);

            CollectionAssert.AreEqual(array, arrayCopy);
        }

        [TestMethod]
        public void test_MergeSortParallelX1()
        {
            MergeSortParallelTestWrapper(1);
        }

        [TestMethod]
        public void test_MergeSortParallelX2()
        {
            MergeSortParallelTestWrapper(2);
        }

        [TestMethod]
        public void test_MergeSortParallelX3()
        {
            MergeSortParallelTestWrapper(3);
        }

        [TestMethod]
        public void test_MergeSortParallel_fromX4_toX32()
        {
            for (int i = 4; i <= 32; i++)
            {
                MergeSortParallelTestWrapper(i);
            }
        }
    }
}
