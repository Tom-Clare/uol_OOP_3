﻿using System;
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
                hashes.Add(Operations.getHash(filename));
            }

            if (hashes[0].SequenceEqual(hashes[1]))  // Compare hashes
            {
                // They are the same
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{files_to_hash[0]} and {files_to_hash[1]} are the same");
            }
            else
            {
                // They are not the same
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{files_to_hash[0]} and {files_to_hash[1]} are not the same");

                // need to send it to some method to further analyse
                AnalyseFiles(files_to_hash);
            }
            Console.ResetColor();
        }

        public static void AnalyseFiles(string[] files_to_analyse)
        {
            // Turn filenames into objects we can work with.
            // filetoanalyse FileA = new filetoanalyse(files_to_analyse[0]);
            // filetoanalyse FileB = new filetoanalyse(files_to_analyse[1]);
        }
    }
}
