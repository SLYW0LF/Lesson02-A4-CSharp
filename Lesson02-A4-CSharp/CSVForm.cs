using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson02_A4_CSharp
{
    public partial class CSVForm : Form
    {
        private DataGridView dataGridView1;
        private Button button1;
        private Label label1;
        private ListBox listBox1;
        private List<string[]> rows;
        private CheckBox checkBox1;
        private bool hasHeader;

        public CSVForm(string csv, bool headers)
        {
            InitializeComponent();
           
            this.Text = csv.Split('\\').Last();
            this.hasHeader = headers;

            rows = File.ReadAllLines(csv).Select(row => row.Split(',')).ToList();

            DataTable dt = new DataTable();
            List<String> columns = new List<string>();

            int maxLength = rows.Select(row => row.Length).Max();

            if (headers) { 

                foreach (string col in rows[0])
                {
                    dt.Columns.Add(col);
                    columns.Add(col);
                }

                for(int i = rows[0].Length + 1; i <= maxLength; i++)
                {
                    dt.Columns.Add(i.ToString());
                    columns.Add(i.ToString());
                }

                foreach (string[] row in rows.Skip(1))
                {
                    dt.Rows.Add(row);
                }

              
                dataGridView1.DataSource = dt;

            }
            else {
               

                for(int i = 1; i <= maxLength; i++)
                {
                    dt.Columns.Add(i.ToString());
                    columns.Add(i.ToString());
                }

                rows.ForEach(x => {
                    dt.Rows.Add(x);
                });
                dataGridView1.DataSource = dt;


            }

            listBox1.DataSource = columns;

        }

        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = this.BackColor;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(765, 537);
            this.dataGridView1.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Items.AddRange(new object[] {
            " "});
            this.listBox1.Location = new System.Drawing.Point(783, 61);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(201, 104);
            this.listBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(778, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select column";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(783, 457);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(201, 92);
            this.button1.TabIndex = 3;
            this.button1.Text = "Calculate Distribution";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(783, 180);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(214, 28);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Treat values as strings";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // CSVForm
            // 
            this.ClientSize = new System.Drawing.Size(996, 583);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CSVForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            long val;

            int firstRow = hasHeader ? 1 : 0;
            int columnIndex = listBox1.SelectedIndex;
            bool numeric = false;

            try
            {
                numeric = Int64.TryParse(rows[firstRow][columnIndex], out val);
            }
            catch { }

            if (checkBox1.Checked) { numeric = false; }
            bool stillNumeric = false;

            List<long> numericList = new List<long>();

            if (numeric)
            {
                
                for (int i = firstRow; i < rows.Count(); i++)
                {
                    stillNumeric = Int64.TryParse(rows[i][columnIndex], out val);

                    if (stillNumeric) { numericList.Add(val); }
                    else { break; }
                  
                }

            }

            if (stillNumeric)
            {

                Form2 form2 = new Form2(numericList);
                this.Visible = false;
                form2.ShowDialog();
                this.Visible = true;
            }
            else
            {

                List<string> ds = new List<string>();

                int i = 0;

                if (hasHeader) { i = 1; }

                for(; i < rows.Count(); i++)
                {
                    try
                    {
                        ds.Add(rows[i][listBox1.SelectedIndex]);
                    }
                    catch
                    {
                       
                    }
                }

                Form2 form2 = new Form2(ds);
                this.Visible = false;
                form2.ShowDialog();
                this.Visible = true;
            }
        

           
        }

      
    }
}