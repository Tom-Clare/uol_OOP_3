using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using uol_OOP_3;

namespace uol_OOP_3_Tests
{
    [TestClass]
    public class TestAnalysingFile
    {

        [TestMethod]
        public void testGetLine()
        {
            uol_OOP_3.AnalysingFile file_to_analyse = new uol_OOP_3.AnalysingFile("GitRepositories_1a.txt");
            KeyValuePair<uol_OOP_3.AnalysingFile.line_status, string> result = file_to_analyse.GetLine();
            KeyValuePair<uol_OOP_3.AnalysingFile.line_status, string> expected = new KeyValuePair<uol_OOP_3.AnalysingFile.line_status, string>(uol_OOP_3.AnalysingFile.line_status.line, "A repository is like a folder for your project. Your project's repository contains all of your project's files and stores each file's revision history. You can also discuss and manage your project's work within the repository.");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void testGetLine2()
        {
            uol_OOP_3.AnalysingFile file_to_analyse = new uol_OOP_3.AnalysingFile("GitRepositories_1a.txt");
            file_to_analyse.GetLine();
            KeyValuePair<uol_OOP_3.AnalysingFile.line_status, string> result = file_to_analyse.GetLine();
            KeyValuePair<uol_OOP_3.AnalysingFile.line_status, string> expected = new KeyValuePair<uol_OOP_3.AnalysingFile.line_status, string>(uol_OOP_3.AnalysingFile.line_status.line, "");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void testGetLine3()
        {
            uol_OOP_3.AnalysingFile file_to_analyse = new uol_OOP_3.AnalysingFile("GitRepositories_1a.txt");
            file_to_analyse.GetLine();
            file_to_analyse.GetLine();
            KeyValuePair<uol_OOP_3.AnalysingFile.line_status, string> result = file_to_analyse.GetLine();
            KeyValuePair<uol_OOP_3.AnalysingFile.line_status, string> expected = new KeyValuePair<uol_OOP_3.AnalysingFile.line_status, string>(uol_OOP_3.AnalysingFile.line_status.line, "You can own repositories individually, or you can share ownership of repositories with other people in an organization.");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void testGetLineEndOfFile()
        {
            uol_OOP_3.AnalysingFile file_to_analyse = new uol_OOP_3.AnalysingFile("GitRepositories_1a.txt");
            file_to_analyse.GetLine();
            file_to_analyse.GetLine();
            file_to_analyse.GetLine();
            file_to_analyse.GetLine();
            file_to_analyse.GetLine();
            KeyValuePair<uol_OOP_3.AnalysingFile.line_status, string> result = file_to_analyse.GetLine();
            KeyValuePair<uol_OOP_3.AnalysingFile.line_status, string> expected = new KeyValuePair<uol_OOP_3.AnalysingFile.line_status, string>(uol_OOP_3.AnalysingFile.line_status.end_of_file, "");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void testGetAllLines()
        {
            uol_OOP_3.AnalysingFile file_to_analyse = new uol_OOP_3.AnalysingFile("GitRepositories_1a.txt");
            List<string> result = file_to_analyse.GetAllLines();
            List<string> expected = new List<string>() {
                $"A repository is like a folder for your project. Your project's repository contains all of your project's files and stores each file's revision history. You can also discuss and manage your project's work within the repository.",
                $"",
                $"You can own repositories individually, or you can share ownership of repositories with other people in an organization.",
                $"",
                $"You can restrict who has access to a repository by choosing the repository's visibility. For more information, see \"About repository visibility.\""
            };
            CollectionAssert.AreEqual(expected, result);
        }

        private class AnalysingLineComparer : Comparer<AnalysingWord>
        {
            // This will override how the AnalysingLine instances are compared within MSTest.
            public override int Compare(AnalysingWord a, AnalysingWord b)
            {
                // We need to override the way that AnalysingLine objects are compared.
                int equal = -1; // Assume a < b until proven equal

                // We'll define two objects of the AnalysingLine class equal if all three of their parameters match.
                if (a.id == b.id && a.word == b.word && a.status == b.status)
                {
                    equal = 0;
                }

                return equal;
            }
        }

        [TestMethod]
        public void testGetAnalysingWords()
        {
            string example_line = "This is an example line";
            AnalysingWord[] result = AnalysingFile.GenerateAnalysingWords(example_line);

            AnalysingWord word1 = new AnalysingWord(0, "This", AnalysingWord.statuses.unclassified);
            AnalysingWord word2 = new AnalysingWord(1, "is", AnalysingWord.statuses.unclassified);
            AnalysingWord word3 = new AnalysingWord(2, "an", AnalysingWord.statuses.unclassified);
            AnalysingWord word4 = new AnalysingWord(3, "example", AnalysingWord.statuses.unclassified);
            AnalysingWord word5 = new AnalysingWord(4, "line", AnalysingWord.statuses.unclassified);
            AnalysingWord[] expected = new AnalysingWord[] { word1, word2, word3, word4, word5 };

            CollectionAssert.AreEqual(expected, result, new AnalysingLineComparer());
        }

        [TestMethod]
        public void testGetAnalysingWords2()
        {
            string example_line = "This, is an example line!!";
            AnalysingWord[] result = AnalysingFile.GenerateAnalysingWords(example_line);

            AnalysingWord word1 = new AnalysingWord(0, "This,", AnalysingWord.statuses.unclassified);
            AnalysingWord word2 = new AnalysingWord(1, "is", AnalysingWord.statuses.unclassified);
            AnalysingWord word3 = new AnalysingWord(2, "an", AnalysingWord.statuses.unclassified);
            AnalysingWord word4 = new AnalysingWord(3, "example", AnalysingWord.statuses.unclassified);
            AnalysingWord word5 = new AnalysingWord(4, "line!!", AnalysingWord.statuses.unclassified);
            AnalysingWord[] expected = new AnalysingWord[] { word1, word2, word3, word4, word5 };

            CollectionAssert.AreEqual(expected, result, new AnalysingLineComparer());
        }
    }
}
