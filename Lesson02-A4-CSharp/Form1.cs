using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson02_A4_CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
        }

        private void renderCSV(string csv)
        {
            
            Console.WriteLine(csv);
            CSVForm csvForm = new CSVForm(csv, checkBox1.Checked);
            this.Visible = false;
            csvForm.ShowDialog();
            this.Visible = true;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            timer1.Interval = 1;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 1)
            {
                label1.Text = "Drop a single file";
                return;
            }
            else if (files.Length == 1)
            {
                if (files[0].EndsWith(".csv"))
                {
                    renderCSV(files[0]);
                }
                else
                {
                    label1.Text = "Drop a CSV File";
                    return;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            renderCSV(openFileDialog1.FileName);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
