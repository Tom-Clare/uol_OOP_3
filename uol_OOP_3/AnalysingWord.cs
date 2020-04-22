using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uol_OOP_3
{
    public class AnalysingWord : IEquatable<AnalysingWord>
    {
        public int id { get; set; }
        public string word { get; set; }
        public statuses status { get; set; }
        public enum statuses
        {
            unclassified,
            identical,
            added,
            removed
        }

        public AnalysingWord (int given_id, string given_word, statuses given_status)
        {
            id = given_id;
            word = given_word;
            status = given_status;
        }

        public bool Equals (AnalysingWord other)
        {
            //  This method sets a custom definition of where two AnalysingLine instances are considered 'equal' as far as the program is concerned.
            if (other is null)
            {
                return false;
            }

            return id == other.id && word == other.word && status == other.status;
        }

        public override bool Equals(object obj) => Equals(obj as AnalysingWord);  // Overrides the default Equals to point to the above custom Equals method

        public override int GetHashCode()
        {
            return id.GetHashCode() + word.GetHashCode() + status.GetHashCode();  // Commutative method of returning a hash of all properties
        }
    }
}
