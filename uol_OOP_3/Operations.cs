using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace uol_OOP_3
{
    public static class Operations
    {
        public static byte[] GetHash (string filename)
        {
            //  Return the SHA256 representation of a given filename 
            byte[] hash = new byte[] { };

            SHA256 sha256 = SHA256.Create();
            try
            {
                FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);  // Initialise file access
                fileStream.Position = 0;  // Make sure we're hashing the whole file by putting the pointer to the start
                hash = sha256.ComputeHash(fileStream);
            }
            catch (FileNotFoundException e)
            {
                Environment.Exit(2);  // Exit gracefully with Windows' file not found code
            }

            return hash;
        }

        public static int GetBiggest (int a, int b)
        {
            //  Simply find and return the larger of two integers
            if (a > b)
            {
                return a;
            }
            return b;
        }

        public static string DumpList (List<string> list)
        {
            //  Convert a list of strings into one printable string seperated by a comma and a space
            string dumped_list = "";

            for (int i = 0; i < list.Count; i++)
            {
                dumped_list = String.Concat(dumped_list, list[i]);
                if (i != list.Count - 1)
                {
                    // This is not the last word in the list
                    dumped_list = String.Concat(dumped_list, ", ");
                }
            }

            return dumped_list;
        }
    }
}
