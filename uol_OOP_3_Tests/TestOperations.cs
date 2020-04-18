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
        public void testOperations ()
        {
            int a = 4;
            int b = 7;
            int result = Operations.GetBiggest(a, b);
            int expected = 7;

            Assert.AreEqual(expected, result);
        }
    }
}
