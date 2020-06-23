using Microsoft.VisualStudio.TestTools.UnitTesting;
using shortest_path_array;
using System;

namespace ShortestPathUnitTests
{
    [TestClass]
    public class FindBestPathTest
    {

        [TestMethod]
        public void ValueTest1()
        {
            int[] array = new int[] { 1, 3, -4, 2, -9, 2, 0, 5, 6, 8, 9 };

            ValueTuple<string, int> expected = ("1 3 2 2 5 9", 5);
            var actual = MainProgram.FindBestPath(array);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValueTest2()
        {
            int[] array = new int[] { 1, 2, 0, -1, 0, 2, 0 };

            ValueTuple<string, int> expected = ("-", -1);
            var actual = MainProgram.FindBestPath(array);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValueTest3()
        {
            int[] array = new int[] { 5 };

            ValueTuple<string, int> expected = ("-", 0);
            var actual = MainProgram.FindBestPath(array);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValueTest4()
        {
            int[] array = new int[] { 1, 2, 1, -1, 0, 5, 0 };

            ValueTuple<string, int> expected = ("-", -1);
            var actual = MainProgram.FindBestPath(array);

            Assert.AreEqual(expected, actual);
        }
    }
}
