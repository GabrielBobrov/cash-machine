namespace CashMachine.Notifications.Messages
{
    public class ErrorMessages
    {
        public static string InvalidWithdrawValue(string minimum_multiple) => $"A maquina aceita somente valores multiplos de {minimum_multiple}";

        public const string InvalidNumberBallotsMachine = "A maquina não possui uma quantidade de cedulas para esse valor";
    }
}
