using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Concordancer
{
    class Tokenizer
    {
        //this takes a string and tokenizes it
        //the default format is "(\d \d+)(\w+)" where $1 is index (chapter and verse) and $2 is verse text
        //the return value is therefor a dictionary with the index as a key and a string array containing the words as the value
        public static Dictionary<string, string[]> Tokenize(string line)
        {
            Regex regex = new Regex(@"^(\(.*?\))(.*?)$");
            Match match = regex.Match(line);
            string index = match.Groups[1].Value;
            string verse = match.Groups[2].Value;

            string[] stringSeparators = new string[] { " ", ",", "." };
            string[] words = verse.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, string[]> indexed = new Dictionary<string, string[]>();
            indexed.Add(index, words);

            return indexed;
        }
    }
}
