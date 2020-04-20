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
                    valid = true;  // it's safe to split up our command list because it fit our RegEx.
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

            char[] charSeperator = new char[] { ' ' };  // Seperate with a space only.
            string[] inputs = input.Split(charSeperator, 3, StringSplitOptions.None);
            string[] files_to_hash = new string[] { inputs[1].ToString(), inputs[2].ToString() };

            List<byte[]> hashes = new List<byte[]>();  // Will hold the hash sequences

            foreach (string filename in files_to_hash)
            {
                hashes.Add(Operations.GetHash(filename));
            }

            if (hashes[0].SequenceEqual(hashes[1]))  // Compare hashes
            {
                // They are the same
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{files_to_hash[0]} and {files_to_hash[1]} are the same");
                Console.ResetColor();
            }
            else
            {
                // They are not the same
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{files_to_hash[0]} and {files_to_hash[1]} are not the same");
                Console.ResetColor();

                // need to send it to some method to further analyse
                bool success = AnalyseFiles(files_to_hash);
            }
        }

        public static bool AnalyseFiles(string[] files_to_analyse)
        {
            // Check if filenames will cause errors
            bool instantiable_filenames = AnalysingFile.CheckFiles(files_to_analyse);
            if (!instantiable_filenames)
            {
                return false;
            }

            // Turn filenames into objects we can work with.
            AnalysingFile FileA = new AnalysingFile(files_to_analyse[0]);
            AnalysingFile FileB = new AnalysingFile(files_to_analyse[1]);
            List<string> all_lines_A = FileA.GetAllLines();
            List<string> all_lines_B = FileB.GetAllLines();

            // Get the count of the larger array
            int max_lines = all_lines_B.Count;
            if (all_lines_A.Count > all_lines_B.Count) {
                max_lines = all_lines_A.Count;
            }

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
                    continue;  // Skip the rest of the loop if no comparisons need to be made.
                }

                // generate id, word, status fields for each word of line
                AnalysingLine[] word_list_a = AnalysingFile.GenerateAnalysingLine(line_A);
                AnalysingLine[] word_list_b = AnalysingFile.GenerateAnalysingLine(line_B);

                // First method - ID issues
                int generic_counter = 0;
                int counter_a = 0;
                int counter_b = 0;
                bool change_found = false;
                while (counter_a < word_list_a.Length && counter_b < word_list_b.Length)
                {
                    if (word_list_a[counter_a].word == word_list_b[counter_b].word)
                    {
                        //  Words are the same
                        Console.Write(word_list_a[counter_a].word);
                        counter_a++;
                        counter_b++;
                        generic_counter++;
                    }
                    else if (word_list_a[counter_a + 1].word == word_list_b[counter_b].word)
                    {
                        //  word_list_a[counter_a] has been removed from B
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(word_list_a[counter_a].word);
                        Console.ResetColor();
                        counter_a++;
                        generic_counter++;
                    }
                    else if (counter_a + 2 < word_list_a.Length && word_list_a[counter_a + 2].word == word_list_b[counter_b + 1].word)
                    {
                        //  word_list_a[counter_a] and word_list_a[counter_a + 1] have been removed from B
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(word_list_b[counter_b].word);
                        Console.ResetColor();
                        counter_a++;
                        generic_counter++;
                    }
                    else if (word_list_a[counter_a].word == word_list_b[counter_b + 1].word)
                    {
                        // word_list_b[counter_b] has been added
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(word_list_b[counter_b].word);
                        Console.ResetColor();
                        counter_b++;
                        generic_counter++;
                    }
                    else if (counter_b + 2 < word_list_b.Length && word_list_a[counter_a + 1].word == word_list_b[counter_b + 2].word)
                    {
                        // word_list_b[counter_b] and word_list_b[counter_b + 1] have been added
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(word_list_b[counter_b].word);
                        Console.ResetColor();
                        counter_b++;
                        generic_counter++;
                    }
                    else if (word_list_a[counter_a + 1].word == word_list_b[counter_b + 1].word)
                    {
                        // word A replaced by B
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(word_list_a[counter_a].word);
                        Console.Write(" ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(word_list_b[counter_b].word);
                        Console.ResetColor();
                        counter_a++;
                        counter_b++;
                        generic_counter++;
                    }
                    else
                    {
                        Console.Write("Somethin wacky be goin down, dude.");
                        counter_a++;
                        counter_b++;
                        generic_counter++;
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
            }

            return true;
        }
    }
}
