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

                List<AnalysingLine> removed_words = word_list_a.Except<AnalysingLine>(word_list_b).ToList();
                List<AnalysingLine> added_words = word_list_b.Except<AnalysingLine>(word_list_a).ToList();

                // Copy line B
                AnalysingLine[] combined_lines = new AnalysingLine[word_list_b.Length];
                word_list_b.CopyTo(combined_lines, 0); // To new array starting from position 0
                List<AnalysingLine> combined_lines_list = combined_lines.ToList();
                // Foreach on new array
                foreach (AnalysingLine word in combined_lines_list)
                {
                // If current element's ID is in removed_words:
                //    Insert in this place
                //    Change status of word to removed
                //    Re-fresh array's IDs (probs need to refresh IDs of removed_words and added_words)
                // If current element's ID is in added_words:
                //    Change status of word to added

                // Loop through and display, I think
                }
                


                // First method - ID issues
                int generic_counter = 0;
                int max_words = word_list_a.Count() + added_words.Count() - 1;
                int counter_a = 0;
                int counter_b = 0;
                while (generic_counter <= max_words)
                {
                    // This is where we actually display them
                    if (word_list_a[counter_a].Equals(word_list_b[counter_b]))
                    {
                        Console.Write(word_list_a[counter_a].word);
                        counter_a++;
                        counter_b++;
                        generic_counter++;
                    }
                    else if (removed_words.Contains(word_list_a[counter_a]))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(word_list_a[counter_a].word);
                        Console.ResetColor();
                        counter_a++;
                        generic_counter++;
                    }
                    else if (added_words.Contains(word_list_b[counter_b]))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(word_list_b[counter_b].word);
                        Console.ResetColor();
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

                    if (generic_counter <= max_words) // If this isn't the last word of the line
                    {
                        Console.Write(" ");
                    }
                    
                }
                Console.WriteLine();  // Finish the line

                //      Union the lines
                //      Loop through arrays in a for loop using the some counter
                //      Create an Acounter and Bcounter
                //      Compare lineA[iA] and lineA[iB]
                //      Figure out priority
                //      Increase counter of whichever line had one of their words printed or increment both if they're the same

            }

            return true;
        }
    }
}
