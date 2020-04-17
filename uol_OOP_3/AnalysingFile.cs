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

        public List<string> GetAllLines()
        {
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

        public static AnalysingLine[] GenerateAnalysingLine(string line)
        {
            // We'll first split by space characters
            string[] words = line.Split(' ');

            int i = 0;
            List<AnalysingLine> word_data = new List<AnalysingLine>();
            foreach (string word in words)
            {
                word_data.Add(new AnalysingLine(i, word, AnalysingLine.statuses.unclassified));
                i++;
            }

            return word_data.ToArray();
        }
    }
}
