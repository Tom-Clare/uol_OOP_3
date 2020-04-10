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
                File.Create("log.txt");
            }

            return new LogFile("log.txt");
        }

        public LogFile (string filename) : base (filename)
        {

        }
    }
}
