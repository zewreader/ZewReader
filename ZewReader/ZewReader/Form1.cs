using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using iTextSharp;
using System.IO;


namespace SpeedReader
{
    public partial class Form1 : Form
    {
        int i = 0;
        string[] words;
        char mychar = '\n';
        public Form1()
        {
           
            InitializeComponent();
           

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            OpenFileDialog dlg = new OpenFileDialog();
            string filepath;
            dlg.Filter = "PDF files(*.PDF)|*.PDF|All files(*.*)|*.*";
            axAcroPDF1.LoadFile("");

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filepath = dlg.FileName.ToString();

                string strText = string.Empty;
                try
                {

                    PdfReader reader = new PdfReader(filepath);
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        ITextExtractionStrategy its ;//= new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy(); //LocationTextExtractionStrategy

                        if (radioButton3.Checked)
                        {
                             its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy(); //LocationTextExtractionStrategy
                        }
                        else {
                             its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy(); //LocationTextExtractionStrategy
                        
                        }
                        String s = PdfTextExtractor.GetTextFromPage(reader, page, its);

                        s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
                        strText = strText + s;

                        MyText.Text = strText;

                    }
                    reader.Close();
                    i = 0;
                    words = strText.Split(mychar);
                    progressBar1.Maximum = words.Length;
                    timer1.Stop();
                    timer1.Start();
                    axAcroPDF1.LoadFile(filepath);
                   


                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                if (MyText.TextLength > 0 || MyText2.TextLength > 0)
                {
                    //  AutoClosingMessageBox.Show(words[i], progressBar1.Value.ToString() + "/" + words.Length.ToString(), System.Convert.ToInt32(numberSet.Value) * words[i].Length);
                    progressBar1.Value = i;
                    label4.Text = i.ToString() + " / " + words.Length.ToString();
                    label1.Text = words[i];
                    label1.Text.Replace("\r", string.Empty).Replace("\n", " ");
                    label1.Text.Trim();
                    label1.SelectAll();
                    label1.SelectionAlignment = HorizontalAlignment.Center;
                    i++;
                    if (words[i - 1].Length < 5)
                    {
                        timer1.Interval = 10 * System.Convert.ToInt32(numberSet.Value);
                    }
                    else
                    {
                        timer1.Interval = words[i - 1].Length * System.Convert.ToInt32(numberSet.Value);
                    }

                }
            }
            catch (Exception)
            {

                //throw;
            }



        }



        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {



        }

        private void numberSet_ValueChanged(object sender, EventArgs e)
        {


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    

        private void label1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MyText2.TextLength > 0)
                {

                    i = 0;
                    MyText2.Text.Replace("\r", "\r ");
                    string noline = MyText2.Text.Replace("\r", System.Convert.ToString(mychar));

                    words = noline.Split(mychar);


                    progressBar1.Maximum = words.Length;
                    timer1.Stop();
                    timer1.Start();
                }
            }
            catch (Exception)
            {

                //throw;
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font.FontFamily, System.Convert.ToInt32(numericUpDown1.Value));
        }

        private void MyText2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            timer1.Stop();
            OpenFileDialog dlg = new OpenFileDialog();
            string filepath;
            dlg.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filepath = dlg.FileName.ToString();

                string strText = string.Empty;
                try
                {
                    MyText.Text = File.ReadAllText(dlg.FileName);
                    MyText.Text.Replace("\r", "\r ");
                    string nolstrTextine = MyText.Text.Replace("\r", System.Convert.ToString(mychar)); //.Replace("\r", string.Empty).Replace("\n", " ");

                    i = 0;
                    words = nolstrTextine.Split(mychar);
                    progressBar1.Maximum = words.Length;
                    timer1.Stop();
                    timer1.Start();


                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            i = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "S T O P")
            {
                timer1.Stop();
                button5.Text = "P L A Y";
            }
            else
            {
                timer1.Start();
                button5.Text = "S T O P";
            }

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening_1(object sender, CancelEventArgs e)
        {

        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyText2.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyText2.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyText2.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyText2.Cut();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MyText.SelectAll();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            MyText.Copy();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            axAcroPDF1.Dispose();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mychar = '\n';
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            mychar = ' ';
        }

        private void Method_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }


    }
}
