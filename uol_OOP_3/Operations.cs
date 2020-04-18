using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace uol_OOP_3
{
    public class Operations
    {
        public static byte[] GetHash (string filename)
        {
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
                Environment.Exit(2);  // Exit with Windows' file not found code.
            }

            return hash;
        }

        public static int GetBiggest (int a, int b)
        {
            if (a > b)
            {
                return a;
            }
            return b;
        }
    }
}
