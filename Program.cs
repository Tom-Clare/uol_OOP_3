using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace uol_OOP_2
{
    static class Program
    {
        static Regex rx_help = new Regex(@"^h$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        static Regex rx_diff = new Regex(@"^diff \w+\.txt \w+\.txt$", RegexOptions.Compiled);

        static void Main(string[] args)
        {
            Console.WriteLine("For help, type h");

            string input = "";
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Write your command below.");
                input = Console.ReadLine();
                if (rx_help.Matches(input).Count == 1)  // They input a help command. Give them help!
                {
                    valid = true;
                    Console.WriteLine("To find the difference between two files, type 'diff [filename_1].txt [filename_2].txt' without quote marks.");
                }
                else if (rx_diff.Matches(input).Count == 1)
                {
                    valid = true;  // it's safe to split up our command list because it fit our RegEx.
                    Console.WriteLine("You'd like to find the difference between two files.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Type h for help.");
                }
                // split the input by space and if input[0] is in valid_input, contine wth attempting the command
                // maybe try regex, would be better I think
            }
        }
    }
}
