using FluentAssertions;
using Xunit;

namespace CashMachine.Tests.Domains
{
    public class WithdrawTests
    {
        [Fact]
        public void TestReturnOfNotes()
        {
            var withdraw = new CashMachine.Domains.Withdraw(1, 1, 1, 1);

            withdraw.Should().NotBeNull();
            withdraw.Should().BeOfType<CashMachine.Domains.Withdraw>();
        }
    }
}
