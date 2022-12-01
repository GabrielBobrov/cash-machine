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
            //Arrange
            var cashMachine = new CashMachine.Domains.CashMachine(tenNotes: 1, twentyNotes: 1, fiftyNotes: 1, hundredNotes: 1);
            var cashMachineService = Services.CashMachineServices.New(new Notifier());
            var expectedWithdraw = new Withdraw(tenNotes: 1, twentyNotes: 1, fiftyNotes: 1, hundredNotes: 1);

            //Act
            var withdraw = cashMachineService.GetWithdraw(withdrawValue: 180, machine: cashMachine);

            //Asserts
            withdraw.Should().BeEquivalentTo(expectedWithdraw);
        }

        [Fact]
        public void WithdrawOfEightyShouldBeReturnOneTenNoteAndOneTwentyNotesAndOneFiftyNote()
        {
            //Arrange
            var cashMachine = new CashMachine.Domains.CashMachine(tenNotes: 1, twentyNotes: 1, fiftyNotes: 1, hundredNotes: 0);
            var cashMachineService = Services.CashMachineServices.New(new Notifier());
            var expectedWithdraw = new Withdraw(tenNotes: 1, twentyNotes: 1, fiftyNotes: 1, hundredNotes: 0);

            //Act
            var withdraw = cashMachineService.GetWithdraw(withdrawValue: 80, machine: cashMachine);

            //Asserts
            withdraw.Should().BeEquivalentTo(expectedWithdraw);
        }

        [Fact]
        public void WithdrawOfThirtyShouldBeReturnOneTenNoteAndTwoTwentyNotes()
        {
            //Arrange
            var cashMachine = new CashMachine.Domains.CashMachine(tenNotes: 1, twentyNotes: 1, fiftyNotes: 0, hundredNotes: 0);
            var cashMachineService = Services.CashMachineServices.New(new Notifier());
            var expectedWithdraw = new Withdraw(tenNotes: 1, twentyNotes: 1, fiftyNotes: 0, hundredNotes: 0);

            //Act
            var withdraw = cashMachineService.GetWithdraw(withdrawValue: 30, machine: cashMachine);

            //Asserts
            withdraw.Should().BeEquivalentTo(expectedWithdraw);
        }

        [Fact]
        public void ValidateInvalidWithdrawValue()
        {
            //Arrange
            var cashMachine = new CashMachine.Domains.CashMachine(tenNotes: 1, twentyNotes: 1, fiftyNotes: 0, hundredNotes: 0);
            var cashMachineService = Services.CashMachineServices.New(new Notifier());

            //Act
            cashMachineService.GetWithdraw(withdrawValue: 33, machine: cashMachine);

            //Asserts
            cashMachineService._notifier.GetNotifications()[0].Message.Should().BeEquivalentTo(ErrorMessages.InvalidWithdrawValue(MINIMUM_MULTIPLE.ToString()));
        }

        [Fact]
        public void ValidateInvalidNumberBallotsMachineForWithdrawValue()
        {
            //Arrange
            var cashMachine = new CashMachine.Domains.CashMachine(tenNotes: 1, twentyNotes: 1, fiftyNotes: 0, hundredNotes: 0);
            var cashMachineService = Services.CashMachineServices.New(new Notifier());

            //Act
            cashMachineService.GetWithdraw(withdrawValue: 150, machine: cashMachine);

            //Asserts
            cashMachineService._notifier.GetNotifications()[0].Message.Should().BeEquivalentTo(ErrorMessages.InvalidNumberBallotsMachine);
        }
    }
}
