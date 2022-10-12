using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson02_A4_CSharp
{
    class Bin
    {

        private long from;
        private long to;
        private bool lastBin;

        public Bin(long f, long t)
        {
            if(f <= t) { 
                this.from = f;
                this.to = t;
            }
            else
            {
                this.from = t;
                this.to = f;
            }
            this.lastBin = false;
        }

        public Bin(long f, long t, bool lb)
        {
            if (f <= t)
            {
                this.from = f;
                this.to = t;
            }
            else
            {
                this.from = t;
                this.to = f;
            }
            this.lastBin = lb;
        }

        public bool fallsIn(long x)
        {
            if (lastBin) { 
                return x <= this.to ? (x >= this.from ? true : false) : false; 
            }
            else
            {
                return x < this.to ? (x >= this.from ? true : false) : false;
            }
            
        }


        override public string ToString()
        {
            return this.from.ToString() + " - " + this.to.ToString();
        }

    }
}
