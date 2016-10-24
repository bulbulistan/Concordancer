using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Concordancer
{
    class CreateDataTable
    {
        /* This function takes a file name, reads it in line by line, tokenizes each line and outputs a datatable
         * which contains all the words as values with chapters and verse as keys
         */
        public static DataTable CreateDT(string fl)
        {
            DataTable conc = new DataTable();
            conc.Columns.Add("location", typeof(string));
            conc.Columns.Add("word", typeof(string));           

            //read in the file line by line
            List<string> lines = FileTools.ReadFile(fl);

            //process each line
            foreach (string l in lines)
            {
                //tokenize every line
                Dictionary<string, string[]> indexedwords = Tokenizer.Tokenize(l);

                //each entry in indexedwords consists of location (indexedword.Key) and word (indexedword.Value)
                foreach (KeyValuePair<string, string[]> indexedword in indexedwords)
                {
                    //populate the datatable
                    foreach (string iw in indexedword.Value)
                    {
                        conc.Rows.Add(new Object[] { indexedword.Key, iw });
                    }
                }
            }
            return conc;
        }
    }
}
