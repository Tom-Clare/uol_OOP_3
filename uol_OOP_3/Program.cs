using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace uol_OOP_3
{
    public static class Program
    {
        static Regex regex_help = new Regex(@"^h$", RegexOptions.Compiled | RegexOptions.IgnoreCase);  // Matches a single h character
        static Regex regex_diff = new Regex(@"^diff \w+\.txt \w+\.txt$", RegexOptions.Compiled);  // Matches a correctly formatted diff command

        public static void Main()
        {
            Console.WriteLine("For help, type h");

            string input = "";
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Write your command below.");
                input = Console.ReadLine();
                if (regex_help.Matches(input).Count == 1)  // They input a help command. Give them help!
                {
                    Console.WriteLine("To find the difference between two files, type 'diff [filename_1].txt [filename_2].txt' without quote marks.\nThe files must be in the same directory as the program.");
                }
                else if (regex_diff.Matches(input).Count == 1)  // They've put in a correctly formatted diff command
                {
                    valid = true;  // it's safe to procceed because the command fits our RegEx
                    Equate(input);
                }
                else
                {
                    Console.WriteLine("Invalid input. Type h for help.");
                }
            }
            Console.ReadKey();
        }

        public static void Equate(string input)
        {
            //  Performs a comparison on the two files on a correctly formatted input
            LogFile log = LogFile.Init();
            log.Write("-- Program started --");

            char[] charSeperator = new char[] { ' ' };  // Seperate with a space
            string[] inputs = input.Split(charSeperator, 3, StringSplitOptions.None);
            string[] files_to_hash = new string[] { inputs[1].ToString(), inputs[2].ToString() };  // Only the second and third elements are of interest

            List<byte[]> hashes = new List<byte[]>();  // Will hold the hash sequences

            foreach (string filename in files_to_hash)
            {
                hashes.Add(Operations.GetHash(filename));  // Fill hashes List with file hashes
            }

            if (hashes[0].SequenceEqual(hashes[1]))  // Compare hashes
            {
                // They are the same
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{files_to_hash[0]} and {files_to_hash[1]} are the same");
                log.Write($"{files_to_hash[0]} and {files_to_hash[1]} are the same");
                Console.ResetColor();
            }
            else
            {
                // They are not the same
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{files_to_hash[0]} and {files_to_hash[1]} are not the same");
                log.Write($"{files_to_hash[0]} and {files_to_hash[1]} are not the same");
                Console.ResetColor();

                // need to send it to some method to further analyse
                bool success = AnalyseFiles(files_to_hash, log);
                if (!success)
                    Environment.Exit(2);  // Exit with Windows' File not Found error code
            }
        }

        public static bool AnalyseFiles(string[] files_to_analyse, LogFile log)
        {
            // Check if filenames will cause errors
            bool instantiable_filenames = AnalysingFile.CheckFiles(files_to_analyse);
            if (!instantiable_filenames)
            {
                return false;
            }

            // Turn filenames into data we can work with.
            AnalysingFile FileA = new AnalysingFile(files_to_analyse[0]);
            AnalysingFile FileB = new AnalysingFile(files_to_analyse[1]);
            List<string> all_lines_A = FileA.GetAllLines();
            List<string> all_lines_B = FileB.GetAllLines();

            // Get the count of the larger array
            int max_lines = Operations.GetBiggest(all_lines_A.Count, all_lines_B.Count);

            for (int i = 0; i <= max_lines; i++)  // for every line in the largest file
            {
                string line_A;
                string line_B;
                try
                {
                    line_A = all_lines_A[i];
                }
                catch
                {
                    line_A = "";
                }
                
                try
                {
                    line_B = all_lines_B[i];
                }
                catch
                {
                    line_B = "";
                }

                if (line_A == line_B)
                {
                    Console.WriteLine(line_A);
                    continue;  // Skip the rest of the loop if no comparisons need to be made on this line
                }

                // At this point, we know the lines aren't the same
                log.Write($"Difference found on line {i+1}");

                // generate id, word, status fields for each word of line
                string[] word_list_a = AnalysingFile.GenerateAnalysingLine(line_A);
                string[] word_list_b = AnalysingFile.GenerateAnalysingLine(line_B);

                // Create containers for words of interest existing on this line
                List<string> removed_words = new List<string>();
                List<string> added_words = new List<string>();

                // Begin analysation of the line
                int counter_a = 0;
                int counter_b = 0;
                while (counter_a < word_list_a.Length && counter_b < word_list_b.Length)
                {
                    if (word_list_a[counter_a].word == word_list_b[counter_b].word)
                    {
                        //  Words are the same
                        Console.Write(word_list_a[counter_a].word);  // Display only one of the identical words

                        counter_a++;  // We don't want to compare either word next iteration
                        counter_b++;
                    }
                    else if (word_list_a[counter_a + 1].word == word_list_b[counter_b].word)
                    {
                        //  word_list_a[counter_a] has been removed from B
                        removed_words.Add(word_list_a[counter_a].word);  // Make a note for logging purposes

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(word_list_a[counter_a].word);  // Display removed word
                        Console.ResetColor();

                        counter_a++;
                    }
                    else if (word_list_a[counter_a].word == word_list_b[counter_b + 1].word)
                    {
                        //  word_list_a[counter_a] has been removed from B
                        added_words.Add(word_list_b[counter_b].word);  // Make a note for logging purposes

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(word_list_b[counter_b].word);  // Display removed word
                        Console.ResetColor();

                        counter_b++;
                    }
                    else if (word_list_a[counter_a + 1].word == word_list_b[counter_b + 1].word)
                    {
                        // word A replaced by B
                        removed_words.Add(word_list_a[counter_a].word);  // Make a note for logging purposes
                        added_words.Add(word_list_b[counter_b].word);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(word_list_a[counter_a].word);  // Display removed word
                        Console.Write(" ");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(word_list_b[counter_b].word);  // Display added word
                        Console.ResetColor();

                        counter_a++;  // We don't want to compare either word next iteration
                        counter_b++;
                    }
                    else if (counter_a + 2 < word_list_a.Length && word_list_a[counter_a + 2].word == word_list_b[counter_b + 1].word)
                    {
                        //  word_list_a[counter_a] and word_list_a[counter_a + 1] have been removed from B
                        removed_words.Add(word_list_a[counter_a].word);  // Make a note for logging purposes
                        removed_words.Add(word_list_a[counter_a + 1].word);
                        added_words.Add(word_list_b[counter_b].word);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(word_list_a[counter_a].word);  // Display removed words
                        Console.Write(" ");
                        Console.Write(word_list_a[counter_a + 1].word);
                        Console.Write(" ");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(word_list_b[counter_b].word);  // Display added word
 
                        Console.ResetColor();

                        counter_a++;
                        counter_a++;
                        counter_b++;
                    }
                    else if (counter_b + 2 < word_list_b.Length && word_list_a[counter_a + 1].word == word_list_b[counter_b + 2].word)
                    {
                        //  word_list_a[counter_a] has been removed
                        //  word_list_b[counter_b] and word_list_b[counter_b + 1] have been added
                        added_words.Add(word_list_b[counter_b].word);  // Make a note for logging purposes
                        added_words.Add(word_list_b[counter_b + 1].word);
                        removed_words.Add(word_list_a[counter_a].word);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(word_list_a[counter_a].word);  // Display removed word
                        Console.Write(" ");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(word_list_b[counter_b].word);  // Display added words
                        Console.Write(" ");
                        Console.Write(word_list_b[counter_b + 1].word);

                        Console.ResetColor();

                        counter_b++;
                        counter_b++;
                        counter_a++;
                    }

                    if (counter_a != word_list_a.Length && counter_b != word_list_b.Length)
                    {
                        // If this is not the last word of the line (and display of the line will continue)
                        Console.Write(" ");
                    }
                }

                if (counter_a == word_list_a.Length)
                {
                    // end of line A reached, print rest of B
                    while (counter_b < word_list_b.Length)
                    {
                        Console.Write(word_list_b[counter_b].word);
                        counter_b++;
                        if (counter_b != word_list_b.Length)
                        {
                            // Don't add a space if this is the last word of the line
                            Console.Write(" ");
                        }
                    }
                }
                if (counter_b == word_list_b.Length)
                {
                    // end of line B reached, print rest of A
                    while (counter_a < word_list_a.Length)
                    {
                        Console.Write(word_list_a[counter_a].word);
                        counter_a++;
                        if (counter_a != word_list_a.Length)
                        {
                            // Don't add a space if this is the last word of the line
                            Console.Write(" ");
                        }
                    }
                }
                Console.WriteLine();  // Finish the line

                if (0 < removed_words.Count())
                    log.Write($"Removed word/s: {Operations.DumpList(removed_words)}");

                if (0 < added_words.Count())
                    log.Write($"Added word/s: {Operations.DumpList(added_words)}");
            }

            return true;
        }
    }
}
