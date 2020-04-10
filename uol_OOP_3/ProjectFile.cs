using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace uol_OOP_3
{
    public class ProjectFile
    {
        public string _filename { get; }

        public ProjectFile (string filename)
        {
            // we are given the filename
            if (File.Exists(filename))
            {
                _filename = filename;
            }
            else
            {
                throw new FileNotFoundException("Given file does not exist");
            }
            
            // We want to set our file_handler with the filestream object here

            // We can then have other methods to interact with the file that we can offload to AnalysingFile and LogFile.
        }
    }
}
