using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Concordancer
{
public static class FileTools
{
    public static List<string> ReadFile(string source)
    {
        using (StreamReader sr = File.OpenText(source))
        {
            List<string> lines = new List<string>();
            
            string s = String.Empty;
            while ((s = sr.ReadLine()) != null)
            {
                if (s != String.Empty)
                {
                    lines.Add(s);
                }
            }
            return lines;
        }
    }
    
    public static void WriteFile(string target, string content)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(target, false))
            {
                writer.Write(content);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
}
