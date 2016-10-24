using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Concordancer
{
    public partial class Form1 : Form
    {
        List<string> files = new List<string>(); //all files to process
        List<string> lines = new List<string>(); //all lines in a files 
        SortedDictionary<string, List<string>> finalconcordance;
        StringBuilder concordancetoprint = new StringBuilder();
        public string target; 
        public string content;
        public string fajlnejm;
        public string fajlpes;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    listBox1.Items.Clear();
                    foreach (String suborpath in openFileDialog1.FileNames)
                    {
                        listBox1.Items.Add(suborpath);
                        files.Add(suborpath);
                    }
                }
                catch { }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("Click the Process files button to create the index");
            }

            else
            {
                saveFileDialog1.ShowDialog();
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string target = Path.Combine(fajlpes, saveFileDialog1.FileName);
            FileTools.WriteFile(target, richTextBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0) {
                MessageBox.Show("Select a file to process");
            }

            else { 
            
            richTextBox1.Text = "";
            
            //So we have a datable for each file which contains the list of words accompanies by indices.
            //This dictionary puts them together with the file name as the key and the datatable as the value.
            Dictionary<string, DataTable> allconcsdatatable = new Dictionary<string, DataTable>();

            foreach (string fajl in files)
            {
                fajlnejm = Path.GetFileName(fajl);
                fajlpes = Path.GetDirectoryName(fajl);

                //create a datatable from the file
                DataTable conc = CreateDataTable.CreateDT(fajl);

                //add it to the big dictionary
                allconcsdatatable.Add(fajl, conc);
            }

            if (radioButton1.Checked) //if an index file for each input file is required
            {
                foreach (KeyValuePair<string, DataTable> ac in allconcsdatatable)
                {
                    //creating concordance
                    finalconcordance = ConcordanceCreator.CreateConcordance(ac.Value);
                }

                //output concordance
                foreach (KeyValuePair<string, List<string>> entry in finalconcordance)
                {
                    string dt = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                    string t = "concordance-" + dt + ".txt";
                    content = entry.Key.ToString() + "\t" + String.Join(";", entry.Value);
                    concordancetoprint.Append(content + "\n");
                }

                richTextBox1.Text = fajlnejm + "\n--------\n";
                richTextBox1.AppendText(concordancetoprint.ToString() + "\n--------\n");
            }

            else if (radioButton2.Checked) //if a single file for all input files is required
            {
                //This datatable combines all the datatable for all files
                DataTable combineddt = new DataTable();
                foreach (KeyValuePair<string, DataTable> ac in allconcsdatatable)
                {
                    combineddt.Merge(ac.Value);
                }
                finalconcordance = ConcordanceCreator.CreateConcordance(combineddt);

                //output concordance
                foreach (KeyValuePair<string, List<string>> entry in finalconcordance)
                {
                    string dt = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                    string t = "concordance-" + dt + ".txt";
                    content = entry.Key.ToString() + "\t" + String.Join(";", entry.Value);
                    concordancetoprint.Append(content + "\n");
                }

                richTextBox1.Text = "Complete concordance\n--------\n";
                richTextBox1.AppendText(concordancetoprint.ToString() + "\n--------\n");
            }                     
        }
        }
    }
}
