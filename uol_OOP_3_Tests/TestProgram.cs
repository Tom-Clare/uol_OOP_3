using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace uol_OOP_3_Tests
{
    [TestClass]
    public class TestProgram
    {
        [TestMethod]
        public void testIdenticalFiles()  // Large test covers the a to z of the program.
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
        public void testDifferingFiles()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);  // Catch output

                uol_OOP_3.Program.Equate("diff GitRepositories_2a.txt GitRepositories_2b.txt");  // Provided files are not identical
                string result = sw.ToString().Trim();
                string expected = @"GitRepositories_2a.txt and GitRepositories_2b.txt are not the same
GitHub's collaborative approach to development depends on publishing commits from your local repository for other people to view, fetch, and update.

A remote URL is Git's fancy way of saying ""the place where your code is stored."" That URL could be your repository on GitHub, or another user's fork, or even on a completely different server.

You can only push to two types of URL url addresses:

An HTTPS URL like https://github.com/user/repo.git
An SSH URL, like git@github.com:user/repo.git
Git associates a remote URL with a name, and your default remote is usually called origin.";
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void testDifferingFiles2()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);  // Catch output

                uol_OOP_3.Program.Equate("diff GitRepositories_3a.txt GitRepositories_3b.txt");  // Provided files are not identical
                string result = sw.ToString().Trim();
                string expected = @"GitRepositories_3a.txt and GitRepositories_3b.txt are not the same
After initializing a pull request, you'll you will see a review page that shows a high-level overview of the changes between your branch (the compare branch) and the repository's base branch. You can add a summary of the proposed changes, review the changes made by commits, add labels, milestones, and assignees, and @mention individual contributors or teams. For more information, see ""Creating a pull request.""

Once you've created a pull request, you can push commits from your topic branch to add them to your the existing pull request. These commits will appear in chronological order within your pull request and the changes will be visible in the ""Files changed"" tab.

Other contributors can review your proposed changes, change, add review comments, contribute to the pull request discussion, and even add commits to the pull request.";
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void testDifferingFiles3()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);  // Catch output

                uol_OOP_3.Program.Equate("diff GitRepositories_3b.txt GitRepositories_3a.txt");  // Provided files are not identical
                string result = sw.ToString().Trim();
                string expected = @"GitRepositories_3b.txt and GitRepositories_3a.txt are not the same
After initializing a pull request, you will you'll see a review page that shows a high-level overview of the changes between your branch (the compare branch) and the repository's base branch. You can add a summary of the proposed changes, review the changes made by commits, add labels, milestones, and assignees, and @mention individual contributors or teams. For more information, see ""Creating a pull request.""

Once you've created a pull request, you can push commits from your topic branch to add them to the your existing pull request. These commits will appear in chronological order within your pull request and the changes will be visible in the ""Files changed"" tab.

Other contributors can review your proposed change, changes, add review comments, contribute to the pull request discussion, and even add commits to the pull request.";
                Assert.AreEqual(expected, result);
            }
        }
    }
}
