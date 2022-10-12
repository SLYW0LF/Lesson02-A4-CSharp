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
    public partial class Form2 : Form
    {
      
        public Form2(List<long> numericDataSet)
        {
            InitializeComponent();

            long max = numericDataSet.Max();
            long min = numericDataSet.Min();

            this.chart1.Series.Clear();

            int numOfBins = 5;

            long range = max - min;
            double h = (double) range / numOfBins;
            long lh = (long)h;
            bool fits = h - lh < 10E-06;

            Dictionary<Bin, int> finalDataSet = new Dictionary<Bin, int>(); 
            long lastVal = min;

            for(int i = 0; i < numOfBins-1; i++)
            {
                finalDataSet.Add(new Bin(lastVal, lastVal + lh), 0);
                lastVal += lh;
            }
            
            if (fits)
            {
                finalDataSet.Add(new Bin(lastVal, lastVal + lh), 0);
            }
            else
            {
                finalDataSet.Add(new Bin(lastVal, lastVal + lh), 0);
                lastVal += lh;
                finalDataSet.Add(new Bin(lastVal, max, true), 0);
            }

            List<Bin> tmp = finalDataSet.Keys.ToList();

            foreach(long d in numericDataSet)
            { 
                foreach (Bin b in tmp)
                {
                    if (b.fallsIn(d))
                    {
                        finalDataSet[b] += 1;
                        break;
                    }
                }

            }

            chart1.Series.Add("Serie");

            foreach (Bin b in finalDataSet.Keys)
            {
                chart1.Series["Serie"].Points.AddXY(b.ToString(), finalDataSet[b]);
            }

        }

        public Form2(List<string> stringDataSet)
        {

            InitializeComponent();

            this.chart1.Series.Clear();

            Dictionary<string, int> finalDataSet = new Dictionary<string, int>();

            foreach (string s in stringDataSet)
            {

                int val;

                if(finalDataSet.TryGetValue(s, out val))
                {
                    finalDataSet[s] += 1;
                }
                else
                {
                    finalDataSet.Add(s, 1);
                }
            }

            chart1.Series.Add("Serie");

            foreach(string k in finalDataSet.Keys)
            {
                Console.WriteLine(k);
                chart1.Series["Serie"].Points.AddXY(k, finalDataSet[k]);
            }


        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
