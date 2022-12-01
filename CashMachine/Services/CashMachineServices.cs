using CashMachine.Domains;
using CashMachine.Interfaces;
using CashMachine.Notifications;
using CashMachine.Notifications.Messages;

namespace CashMachine.Services
{
    public class CashMachineServices
    {
        private const int MINIMUM_MULTIPLE = 10;

        public  INotifier _notifier { get; set; }
        private CashMachineServices(INotifier notifier)
        {
            _notifier = notifier;
        }

        public static CashMachineServices New(INotifier notifier) => new CashMachineServices(notifier);
  
        public Withdraw? GetWithdraw(int withdrawValue, Domains.CashMachine machine)
        {
            var tenNotes = 0;
            var twentyNotes = 0;
            var fiftyNotes = 0;
            var hundredNotes = 0;

            if (!(withdrawValue % MINIMUM_MULTIPLE == 0))
            {
                NotifyError(ErrorMessages.InvalidWithdrawValue(MINIMUM_MULTIPLE.ToString()));
                return null;
            }                

            if (machine.HundredNotes > 0)
            {
                var hundredNotesQuantity = Math.DivRem(withdrawValue, 100, out int hundredRest);

                if (machine.HundredNotes - hundredNotesQuantity >= 0)
                {
                    hundredNotes += hundredNotesQuantity;
                    withdrawValue = hundredRest;
                    machine.HundredNotes -= hundredNotesQuantity;
                }
            }

            if (machine.FiftyNotes > 0)
            {
                var fiftyNotesQuantity = Math.DivRem(withdrawValue, 50, out int fiftyRest);

                if (machine.FiftyNotes - fiftyNotesQuantity >= 0)
                {
                    fiftyNotes += fiftyNotesQuantity;
                    withdrawValue = fiftyRest;
                    machine.FiftyNotes -= fiftyNotesQuantity;
                }
            }

            if (machine.TwentyNotes > 0)
            {
                var twentyNotesQuantity = Math.DivRem(withdrawValue, 20, out int twentyRest);

                if (machine.TwentyNotes - twentyNotesQuantity >= 0)
                {
                    twentyNotes += twentyNotesQuantity;
                    withdrawValue = twentyRest;
                    machine.TwentyNotes -= twentyNotesQuantity;
                }
            }

            if (machine.TenNotes > 0)
            {
                var tenNotesQuantity = Math.DivRem(withdrawValue, 10, out int tenRest);

                if (machine.TenNotes - tenNotesQuantity >= 0)
                {
                    tenNotes += tenNotesQuantity;
                    withdrawValue = tenRest;
                    machine.TenNotes -= tenNotesQuantity;
                }
            }

            if (withdrawValue > 0)
            {
                NotifyError(ErrorMessages.InvalidNumberBallotsMachine);
                return null;
            }

            return new Withdraw(tenNotes, twentyNotes, fiftyNotes, hundredNotes);
        }

        protected void NotifyError(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }
}
