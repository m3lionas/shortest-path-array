using Microsoft.VisualStudio.TestTools.UnitTesting;
using shortest_path_array;


namespace ShortestPathUnitTests
{
    [TestClass]
    public class ShortestPathValueTest
    {

        [TestMethod]
        public void ValueTest1()
        {
            int[] array = new int[] { 1, 3, -4, 2, -9, 2, 0, 5, 6, 8, 9 };
            int expected = 5;

            int actual = MainProgram.jumpy(array);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void ValueTest2()
        {
            int[] array = new int[] { 1, 2, 0, -1, 0, 2, 0 };
            int expected = -1;

            int actual = MainProgram.jumpy(array);

            Assert.AreEqual(expected, actual);

        }
    }
}
