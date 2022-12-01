using CashMachine.Domains;
using CashMachine.Notifications;
using CashMachine.Notifications.Messages;
using FluentAssertions;
using Xunit;

namespace CashMachine.Tests
{
    public class CashMachineServicesTests
    {
        private const int MINIMUM_MULTIPLE = 10;

        [Fact]
        public void WithdrawOfOneHundredEightyShouldBeReturnOneTenNoteAndOneTwentyNotesAndOneFiftyNoteAndOneHundredNote()
        {
            var cashMachine = new CashMachine.Domains.CashMachine(tenNotes: 1, twentyNotes: 1, fiftyNotes: 1, hundredNotes: 1);
            var cashMachineService = Services.CashMachineServices.New(new Notifier());
            var withdraw = cashMachineService.GetWithdraw(withdrawValue: 180, machine: cashMachine);

            var expectedWithdraw = new Withdraw(tenNotes: 1, twentyNotes: 1, fiftyNotes: 1, hundredNotes: 1);

            withdraw.Should().BeEquivalentTo(expectedWithdraw);
        }

        [Fact]
        public void WithdrawOfEightyShouldBeReturnOneTenNoteAndOneTwentyNotesAndOneFiftyNote()
        {
            var cashMachine = new CashMachine.Domains.CashMachine(tenNotes: 1, twentyNotes: 1, fiftyNotes: 1, hundredNotes: 0);
            var cashMachineService = Services.CashMachineServices.New(new Notifier());
            var withdraw = cashMachineService.GetWithdraw(withdrawValue: 80, machine: cashMachine);

            var expectedWithdraw = new Withdraw(tenNotes: 1, twentyNotes: 1, fiftyNotes: 1, hundredNotes: 0);

            withdraw.Should().BeEquivalentTo(expectedWithdraw);
        }

        [Fact]
        public void WithdrawOfThirtyShouldBeReturnOneTenNoteAndTwoTwentyNotes()
        {
            var cashMachine = new CashMachine.Domains.CashMachine(tenNotes: 1, twentyNotes: 1, fiftyNotes: 0, hundredNotes: 0);
            var cashMachineService = Services.CashMachineServices.New(new Notifier());
            var withdraw = cashMachineService.GetWithdraw(withdrawValue: 30, machine: cashMachine);

            var expectedWithdraw = new Withdraw(tenNotes: 1, twentyNotes: 1, fiftyNotes: 0, hundredNotes: 0);

            withdraw.Should().BeEquivalentTo(expectedWithdraw);
        }

        [Fact]
        public void ValidateInvalidWithdrawValue()
        {
            var cashMachine = new CashMachine.Domains.CashMachine(tenNotes: 1, twentyNotes: 1, fiftyNotes: 0, hundredNotes: 0);
            var cashMachineService = Services.CashMachineServices.New(new Notifier());
            cashMachineService.GetWithdraw(withdrawValue: 33, machine: cashMachine);

            cashMachineService._notifier.GetNotifications()[0].Message.Should().BeEquivalentTo(ErrorMessages.InvalidWithdrawValue(MINIMUM_MULTIPLE.ToString()));
        }

        [Fact]
        public void ValidateInvalidNumberBallotsMachineForWithdrawValue()
        {
            var cashMachine = new CashMachine.Domains.CashMachine(tenNotes: 1, twentyNotes: 1, fiftyNotes: 0, hundredNotes: 0);
            var cashMachineService = Services.CashMachineServices.New(new Notifier());
            cashMachineService.GetWithdraw(withdrawValue: 150, machine: cashMachine);

            new Withdraw(tenNotes: 1, twentyNotes: 1, fiftyNotes: 0, hundredNotes: 0);

            cashMachineService._notifier.GetNotifications()[0].Message.Should().BeEquivalentTo(ErrorMessages.InvalidNumberBallotsMachine);
        }
    }
}
