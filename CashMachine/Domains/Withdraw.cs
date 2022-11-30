namespace CashMachine.Domains
{
    public class Withdraw
    {

        public Withdraw(int tenNotes, int twentyNotes, int fiftyNotes, int hundredNotes)
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
