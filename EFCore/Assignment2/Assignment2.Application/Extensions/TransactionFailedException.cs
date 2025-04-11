namespace Assignment2.Application.Extensions
{
    public class TransactionFailedException : Exception
    {
        public TransactionFailedException() : base("The transaction failed to complete")
        {
        }

        public TransactionFailedException(string message) : base(message)
        {
        }

        public TransactionFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
