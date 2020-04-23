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
            sr = new StreamReader(filename);  // Now available for all methods
        }

        public KeyValuePair<line_status, string> GetLine()
        {
            // Return single line of the file or tell caller the file has ended
            KeyValuePair<line_status, string> result = new KeyValuePair<line_status, string>();
            var next_line = sr.ReadLine();  // Read the next line from wherever the internal pointer is onwards

            if (next_line == null)
            {
                // This is the last line of the file
                result = new KeyValuePair<line_status, string>(line_status.end_of_file, "");
            }
            else
            {
                result = new KeyValuePair<line_status, string>(line_status.line, next_line);
            }

            return result;
        }

        public List<string> GetAllLines()
        {
            // Get every line of the file and return in List of strings
            List<string> all_lines = new List<string>();
            bool end_of_file = false;
            while (!end_of_file)
            {
                KeyValuePair<line_status, string> line = GetLine();
                if (line.Key == line_status.end_of_file)
                {
                    end_of_file = true;
                }
                else
                {
                    all_lines.Add(line.Value);
                }
            }

            return all_lines;
        }

        public static bool CheckFiles (string[] filenames)
        {
            // Attempt to instantiate files and return the all clear if no errors were generated
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

        public static string[] GenerateAnalysingLine(string line)
        {
            // Split given string by space and return as array
            return line.Split(' ');
        }
    }
}
