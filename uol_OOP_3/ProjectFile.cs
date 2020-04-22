using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace uol_OOP_3
{
    public abstract class ProjectFile
    {
        public string _filename { get; }

        public ProjectFile (string filename)
        {
            //  Check the file exists before allowing instantiation
            if (File.Exists(filename))
            {
                _filename = filename;
            }
            else
            {
                throw new FileNotFoundException("Given file does not exist");
            }
        }
    }
}
