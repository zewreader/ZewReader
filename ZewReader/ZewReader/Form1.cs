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
using Code7248.word_reader;
using iTextSharp.text;


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

            iTextSharp.text.FontFactory.RegisterDirectories();
           
 
            timer1.Stop();
            button5.Text = "P L A Y";
            OpenFileDialog dlg = new OpenFileDialog();
            string filepath;
            dlg.Filter = "PDF files(*.PDF)|*.PDF|All files(*.*)|*.*";
           // axFoxitCtl1.OpenFile("");

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
                        String s;
                        s = PdfTextExtractor.GetTextFromPage(reader, page, its);

                        if (false)
                        {
                            var fTahoma = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H);

                            var pi = new Phrase(s, fTahoma);
                            s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(pi.Content)));
                            char[] charArray = s.ToCharArray();
                            Array.Reverse(charArray);
                            s = new string(charArray);
                        }

                       var ss = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));

                       
                        //s = s.ToString;
                        strText = strText + " " +ss;
                        MyText.Text = strText;
                        //MyText.RightToLeft = MyText.Fon

                    }
                    reader.Close();
                    i = 0;
                   strText = strText.Replace("\t", " ");
                   strText = strText.Replace("  ", " ");
                  strText =  strText.Replace("\u2000", " ");
                    strText = strText.Replace(Environment.NewLine, "\n");
                    if (mychar == '\n')
                    {
                        strText = strText.Replace("\n", " ");
                        strText = strText.Replace(".", ". " + mychar);
                    }
                    words = strText.Split(new char[] { mychar}, StringSplitOptions.RemoveEmptyEntries);
                    progressBar1.Maximum = words.Length;
                    timer1.Stop();
                    button5.Text = "P L A Y";
                    System.Diagnostics.Process.Start(filepath);
                    //axFoxitCtl1.OpenFile();
                    //axAcroPDF2.LoadFile(filepath);
                    //axAcroPDF2.src = filepath;
                    //axAcroPDF2.Show();
                   


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
                        label1.Text ="";
                    if (checkBox1.Checked)
                    {
                        var myword = words[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        for (int k = 0; k < myword.Length; k++)
                        {
                            if (myword[k].Length <= 3 && myword[k].ToLower() != "I" && myword[k].ToLower() != "he" && myword[k].ToLower() != "she"
                                && myword[k].ToLower() != "we" && myword[k].ToLower() != "you")
                                myword[k] = null;
                        }
                        for (int k = 0; k < myword.Length; k++)
                        {
                            label1.Text += myword[k] + ' ';
                        }
                    }
                    else
                    {
                        label1.Text = words[i];
                    }
                   // label1.Text.Replace("\r", string.Empty).Replace("\n", " ");
                    //label1.Text.Trim();
                    label1.SelectAll();
                    label1.SelectionAlignment = HorizontalAlignment.Center;
                    i++;
                    int lenval= System.Convert.ToInt32(numberSet.Value);

                    int myval = words[i - 1].Length;

                    if (myval < 5)
                    {
                        timer1.Interval = 10 * lenval;
                    }
                    else if (myval > 3000)
                    {
                        timer1.Interval = 3000 * lenval;
                    }
                    else if (myval <= 3000 && myval >= 5)
                    {
                        timer1.Interval = words[i - 1].Length * lenval;
                    }

                }
            }
            catch (Exception ex)
            {
                timer1.Stop();
                button5.Text = "P L A Y";
              //  MessageBox.Show(ex.Message);
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
                   MyText2.Text = MyText2.Text.Replace(Environment.NewLine, "\n");
                   MyText2.Text = MyText2.Text.Replace("\t", "\n");
                   if (mychar == '\n')
                   {
                       MyText2.Text = MyText2.Text.Replace("\n", " ");
                   }
                   
                    string noline = MyText2.Text.Replace("\n", System.Convert.ToString(mychar));
                    if (mychar == '\n')
                    {
                        noline = noline.Replace(".", ". " + mychar);
                    }
                    words = noline.Split(new char[] { mychar }, StringSplitOptions.RemoveEmptyEntries);


                    progressBar1.Maximum = words.Length;
                    timer1.Stop();
                    button5.Text = "P L A Y";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            label1.Font = new System.Drawing.Font(label1.Font.FontFamily, System.Convert.ToInt32(numericUpDown1.Value));
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
            button5.Text = "P L A Y";
            OpenFileDialog dlg = new OpenFileDialog();
            string filepath;
            dlg.Filter = "Text files(*.txt)|*.txt|Doc files(*.doc)|*.doc|Docx files(*.docx)|*.docx|All files(*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filepath = dlg.FileName.ToString();

                string strText = string.Empty;
                try
                {

                    string ext = System.IO.Path.GetExtension(dlg.FileName);
                  
                    if (ext == ".doc" || ext == ".docx")
                    {
                        Code7248.word_reader.TextExtractor extractor = new TextExtractor(dlg.FileName);

                        string contents = extractor.ExtractText();
                        MyText.Text = contents;
                    }
                    else
                    {

                        MyText.Text = File.ReadAllText(dlg.FileName);
                        
                    }
                   MyText.Text =  MyText.Text.Replace("\t", " ");
                    MyText.Text = MyText.Text.Replace(Environment.NewLine, "\n");
                    if (mychar == '\n')
                    {
                        MyText.Text = MyText.Text.Replace("\n", " ");
                    }
                    string nolstrTextine = MyText.Text.Replace("\n", System.Convert.ToString(mychar));
                    i = 0;
                    if (mychar == '\n')
                    {
                        nolstrTextine = nolstrTextine.Replace(".", ". " + mychar);
                    }
                
                    words = nolstrTextine.Split(new char[] { mychar }, StringSplitOptions.RemoveEmptyEntries); ;
              
                    progressBar1.Maximum = words.Length;

                    timer1.Stop();
                    button5.Text = "P L A Y";


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
                int lenval = System.Convert.ToInt32(numberSet.Value);
                timer1.Interval = lenval+30;
                timer1.Stop();
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
            //axAcroPDF2.Dispose();
            //axAcroPDF2 = null;
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
