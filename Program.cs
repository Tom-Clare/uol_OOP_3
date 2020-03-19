using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;

namespace uol_OOP_2
{
    static class Program
    {
        static Regex regex_help = new Regex(@"^h$", RegexOptions.Compiled | RegexOptions.IgnoreCase);  // Matches a single h character
        static Regex regex_diff = new Regex(@"^diff \w+\.txt \w+\.txt$", RegexOptions.Compiled);  // Matches a correctly formatted diff command

        static void Main(string[] args)
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
                    valid = true;
                    Console.WriteLine("To find the difference between two files, type 'diff [filename_1].txt [filename_2].txt' without quote marks.\nThe files must be in the same directory as the program.");
                }
                else if (regex_diff.Matches(input).Count == 1)  // They've put in a correctly formatted diff command
                {
                    valid = true;  // it's safe to split up our command list because it fit our RegEx.

                    char[] charSeperator = new char[] { ' ' };  // Seperate with a space only.
                    string[] inputs = input.Split(charSeperator, 3, StringSplitOptions.None);
                    string[] files_to_hash = new string[] { inputs[1].ToString(), inputs[2].ToString() };

                    List<byte[]> hashes = new List<byte[]>();  // Will hold the hash sequences

                    using (SHA256 sha256 = SHA256.Create())
                    {
                        foreach (string filename in files_to_hash)
                        {
                            FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);  // Initialise file access
                            fileStream.Position = 0;  // Make sure we're hashing the whole file by putting the pointer to the start
                            byte[] hash = sha256.ComputeHash(fileStream);
                            hashes.Add(hash);
                        }
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
                    }
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Invalid input. Type h for help.");
                }
            }
            Console.ReadKey();
        }
    }
}
