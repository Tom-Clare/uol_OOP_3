using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uol_OOP_3;

namespace uol_OOP_3_Tests
{
    [TestClass]
    public class TestOperations
    {
        [TestMethod]
        public void testGetBiggest ()
        {
            int a = 4;
            int b = 7;
            int result = Operations.GetBiggest(a, b);
            int expected = 7;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void testDumpList()
        {
            List<string> example_list = new List<string>() { "The", "list", "method", "should", "work", "as", "planned" };
            string result = Operations.DumpList(example_list);
            string expected = "The, list, method, should, work, as, planned";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void testDumpList2()
        {
            // Edge cases
            List<string> example_list = new List<string>() { "Yellow", ";", "()", ".......", "5" };
            string result = Operations.DumpList(example_list);
            string expected = "Yellow, ;, (), ......., 5";

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void testDumpList3()
        {
            // Another edge case
            List<string> example_list = new List<string>() { "Yellow" };
            string result = Operations.DumpList(example_list);
            string expected = "Yellow";

            Assert.AreEqual(expected, result);
        }
    }
}
