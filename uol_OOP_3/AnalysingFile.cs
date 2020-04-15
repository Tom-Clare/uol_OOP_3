using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace uol_OOP_3
{
    public class AnalysingFile : ProjectFile
    {
        public StreamReader sr;
        public enum line_status
        {
            line,
            end_of_file
        }

        public AnalysingFile(string filename) : base(filename)
        {
            sr = new StreamReader(filename);
        }

        public KeyValuePair<line_status, string> GetLine()
        {
            KeyValuePair<line_status, string> result = new KeyValuePair<line_status, string>();
            var next_line = sr.ReadLine();

            if (next_line == null)
            {
                result = new KeyValuePair<line_status, string>(line_status.end_of_file, "");
            }
            else
            {
                result = new KeyValuePair<line_status, string>(line_status.line, next_line);
            }
            return result;
        }

        public static bool CheckFiles (string[] filenames)
        {
            try
            {
                AnalysingFile FileA = new AnalysingFile(filenames[0]);
            }
            catch (FileNotFoundException e)
            {
                return false;
            }

            try
            {
                AnalysingFile FileB = new AnalysingFile(filenames[1]);
            }
            catch (FileNotFoundException e)
            {
                return false;
            }

            return true;
        }
    }
}
