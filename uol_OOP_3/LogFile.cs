using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace uol_OOP_3
{
    public class LogFile : ProjectFile
    {
        public static LogFile Init ()
        {
            if (!File.Exists("log.txt"))
            {
                File.Create("log.txt").Dispose();  // Dispose saves the file onto the system
            }

            return new LogFile("log.txt");
        }

        private LogFile (string filename) : base (filename)
        {

        }

        public void Write (string message)
        {
            //  Append given message to the end of log.txt
            using (System.IO.StreamWriter sw = File.AppendText("log.txt"))
            {
                sw.WriteLine(message);  // Appends to end of string. Lines are delimited by Environment.NewLine
            }
        }

        public void Clear ()
        {
            //  Deletes all content in log.txt
            File.WriteAllText("log.txt", String.Empty);  // Replaces all text with an empty string
        }
    }
}
