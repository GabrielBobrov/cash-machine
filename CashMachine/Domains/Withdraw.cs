namespace CashMachine.Domains
{
    public class Withdraw : Base
    {
        public Withdraw(int tenNotes, int twentyNotes, int fiftyNotes, int hundredNotes) : base(tenNotes, twentyNotes, fiftyNotes, hundredNotes)
        {
        }
    }
}
