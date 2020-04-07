using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace uol_OOP_3_Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Test_Identical_Files()  // Large test covers the a to z of the program.
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);  // Catch output

                uol_OOP_3.Program.Equate("diff GitRepositories_1a.txt GitRepositories_1b.txt");  // Provided files are identical
                string result = sw.ToString().Trim();
                string expected = "GitRepositories_1a.txt and GitRepositories_1b.txt are the same";
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void Test_Differing_Files()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);  // Catch output

                uol_OOP_3.Program.Equate("diff GitRepositories_1a.txt GitRepositories_2a.txt");  // Provided files are not identical
                string result = sw.ToString().Trim();
                string expected = "GitRepositories_1a.txt and GitRepositories_2a.txt are not the same";  // Might change as development progresses.
                Assert.AreEqual(expected, result);
            }
        }
    }
}
