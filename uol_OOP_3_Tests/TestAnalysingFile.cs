using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
    }
}
