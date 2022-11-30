namespace CashMachine.Notifications.Messages
{
    public class ErrorMessages
    {
        public static string InvalidWithdrawValue(string minimumMultiple) => $"A maquina aceita somente valores multiplos de {minimumMultiple}";

        public const string InvalidNumberBallotsMachine = "A maquina não possui uma quantidade de cedulas para esse valor";
    }
}
