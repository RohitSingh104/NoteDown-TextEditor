using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {


        int i = 1;
        Hashtable filetable = new Hashtable();
        

        public Form1()
        {
            InitializeComponent();

        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            TabPage tp = new TabPage("NewTab" + i);
            i++;
            filetable.Add(tp.Text, "");

            RichTextBox rtb = new RichTextBox();
            rtb.Dock = DockStyle.Fill;
            tp.Controls.Add(rtb);
            tabControl1.TabPages.Add(tp);

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Undo();
        }

        private RichTextBox GetRichTextBox()
        {
            RichTextBox rtb = null;
            TabPage tp = tabControl1.SelectedTab;

            if (tp != null)
            {
                rtb = tp.Controls[0] as RichTextBox;
            }

            return rtb;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tp = new TabPage("NewTab" + i);
            i++;

            filetable.Add(tp.Text, "");
            RichTextBox rtb = new RichTextBox();
            rtb.Dock = DockStyle.Fill;
            tp.Controls.Add(rtb);
            tabControl1.TabPages.Add(tp);



        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().SelectAll();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox1 = GetRichTextBox();
            openFileDialog1.ShowDialog();
            string fn = openFileDialog1.FileName;
            System.IO.StreamReader sr = new System.IO.StreamReader(fn);
            richTextBox1.Text = sr.ReadToEnd();
            sr.Close();


        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().WordWrap = !GetRichTextBox().WordWrap;
            statusBarToolStripMenuItem.Enabled = !GetRichTextBox().WordWrap;
            if (GetRichTextBox().WordWrap == true)
            {
                wordWrapToolStripMenuItem.Checked = true;
                statusBarToolStripMenuItem.Checked = false;
                GetRichTextBox().Visible = false;
            }
            else
            {
                statusBarToolStripMenuItem.Checked = true;
                GetRichTextBox().Visible = true;
                wordWrapToolStripMenuItem.Checked = false;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox1 = GetRichTextBox();
            fontDialog1.ShowDialog();
            richTextBox1.SelectionFont = fontDialog1.Font;
            richTextBox1.Focus();
        }



        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox1 = GetRichTextBox();
            colorDialog1.ShowDialog();
            richTextBox1.SelectionColor = colorDialog1.Color;
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m > 7)
            {
                m = GetRichTextBox().Font.Size;
                GetRichTextBox().Font = new Font(GetRichTextBox().Font.Name, m - 3);
            }
            else if (m >= 8)
            {
                zoomOutToolStripMenuItem.Enabled = false;
            }
        }

        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Font = new Font(GetRichTextBox().Font.Name, 13);
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!wordWrapToolStripMenuItem.Checked)
            {
                statusBarToolStripMenuItem.Checked = !statusBarToolStripMenuItem.Checked;
                if (statusBarToolStripMenuItem.Checked)
                {

                    statusStrip1.Visible = true;

                }
                else
                {

                    statusStrip1.Visible = false;
                }
            }
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            RichTextBox rtb = GetRichTextBox();
            TabPage tp = tabControl1.SelectedTab;
            string fName, fullPath, oldName;
            if (filetable[tp.Text].ToString() == "")
            {
                SaveFileDialog sv1 = new SaveFileDialog();
                sv1.DefaultExt = "rtf";
                sv1.Filter = "RTF File(*.rtf)|*.rtf|All File(*.*)|*.* ";
                if (sv1.ShowDialog() == DialogResult.OK)
                {
                    oldName = tp.Text;
                    rtb.SaveFile(sv1.FileName);
                    fName = System.IO.Path.GetFileName(sv1.FileName);
                    tp.Text = fName;
                    fullPath = sv1.FileName.ToString();

                    filetable.Remove(oldName);
                    filetable.Add(fName, fullPath);

                }
            }
            else
            {
                rtb.SaveFile(filetable[tp.Text].ToString());
            }


        }
        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pageSetupDialog1.ShowDialog();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void toolStripSeparator2_Click(object sender, EventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox1 = GetRichTextBox();
            saveFileDialog1.Filter = "Text File(*.txt)|*.txt|All Files(*.*)|*.*|RTF File(*.rtf)|*.rtf";
            saveFileDialog1.FilterIndex = 2;
            if(saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
                TabPage tp = new TabPage();
                tabControl1.SelectedTab.Text = System.IO.Path.GetFileName(saveFileDialog1.FileName);
                String na = saveFileDialog1.FileName;
                richTextBox1.LoadFile(na);
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = 0;
            String txt = GetRichTextBox().Text;
            GetRichTextBox().Text = "";
            GetRichTextBox().Text = txt;
            while(index < GetRichTextBox().Text.LastIndexOf(toolStripTextBox1.Text))
            {
                GetRichTextBox().Find(toolStripTextBox1.Text, index, GetRichTextBox().TextLength, RichTextBoxFinds.None);
                GetRichTextBox().SelectionBackColor = Color.GreenYellow;
                index = GetRichTextBox().Text.IndexOf(toolStripTextBox1.Text, index) + 1;
            }

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(GetRichTextBox().Text, new Font("Arial", 20), Brushes.Black, 12, 20);
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            foreach(FontFamily font in FontFamily.Families)
            {
                toolStripComboBox1.Items.Add(font.Name.ToString());
            }
           

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Font of;
            Font nf;
            RichTextBox richTextBox1 = GetRichTextBox();
            of=richTextBox1.SelectionFont;
            if (of.Italic)
                nf = new Font(of, of.Style & FontStyle.Bold);
            else
                nf = new Font(of, of.Style | FontStyle.Bold);
            richTextBox1.SelectionFont = nf;
            richTextBox1.Focus();
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Font of;
            Font nf;
            RichTextBox richTextBox1 = GetRichTextBox();
            of = richTextBox1.SelectionFont;
            if (of.Italic)
                nf = new Font(of, of.Style & FontStyle.Italic);
            else
                nf = new Font(of, of.Style | FontStyle.Italic);
            richTextBox1.SelectionFont = nf;
            richTextBox1.Focus();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Font of;
            Font nf;
            RichTextBox richTextBox1 = GetRichTextBox();
            of = richTextBox1.SelectionFont;
            if (of.Underline)
                nf = new Font(of, of.Style & FontStyle.Underline);
            else
                nf = new Font(of, of.Style | FontStyle.Underline);
            richTextBox1.SelectionFont = nf;
            richTextBox1.Focus();
        }
    }
}