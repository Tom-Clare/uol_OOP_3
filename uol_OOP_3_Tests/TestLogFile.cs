using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uol_OOP_3;

namespace uol_OOP_3_Tests
{
    [TestClass]
    public class TestLogFile
    {
        [TestMethod]
        public void testLogFileConstructor ()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);  // Catch output

                LogFile log = LogFile.Init();
                string result = log._filename;
                string expected = "log.txt";
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void testLogFileWriter()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);  // Catch output

                LogFile log = LogFile.Init();
                log.Clear();
                log.Write("This is a message");
                string result = "";
                using (StreamReader file = File.OpenText("log.txt"))
                {
                    result = file.ReadToEnd();
                }
                string expected = $"This is a message{Environment.NewLine}";
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void testLogFileWriter2()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);  // Catch output

                LogFile log = LogFile.Init();
                log.Clear();
                log.Write("This is a message");
                log.Write("This is another message");
                string result = "";
                using (StreamReader file = File.OpenText("log.txt"))
                {
                    result = file.ReadToEnd();
                }
                string expected = $"This is a message{Environment.NewLine}This is another message{Environment.NewLine}";
                Assert.AreEqual(expected, result);
            }
        }
    }
}
