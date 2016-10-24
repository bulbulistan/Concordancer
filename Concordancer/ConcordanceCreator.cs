using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Concordancer
{
    class ConcordanceCreator
    {
        public static SortedDictionary<string, List<string>> CreateConcordance(DataTable conc)
        {
            //this is where the concordance is stored
            SortedDictionary<string, List<string>> final = new SortedDictionary<string, List<string>>();

            //process each row 
            foreach (DataRow r in conc.Rows)
            {
                // Here we swap the pairing: 
                // key is now the word (because we need unique words)
                // value is the location
                string ent = r.ItemArray[1].ToString();
                string vrs = r.ItemArray[0].ToString();

                if (!final.ContainsKey(ent))
                {
                    List<string> lst = new List<string>();
                    lst.Add(vrs);
                    final.Add(ent, lst);
                }

                else
                {
                    List<string> newlst = final[ent];
                    if (newlst.Contains(vrs))
                    {
                        //do nothing                            
                    }
                    else
                    {
                        newlst.Add(vrs);
                        final[ent] = newlst;
                    }
                }
            }
            return final;
        }
    }
}
