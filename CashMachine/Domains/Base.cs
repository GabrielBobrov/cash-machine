using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine.Domains
{
    public abstract class Base
    {
        public Base(int tenNotes, int twentyNotes, int fiftyNotes, int hundredNotes)
        {
            TenNotes = tenNotes;
            TwentyNotes = twentyNotes;
            FiftyNotes = fiftyNotes;
            HundredNotes = hundredNotes;
        }

        public int TenNotes { get; set; }

        public int TwentyNotes { get; set; }

        public int FiftyNotes { get; set; }

        public int HundredNotes { get; set; }
    }
}
