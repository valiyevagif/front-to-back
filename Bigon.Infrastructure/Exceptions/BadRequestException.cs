namespace Bigon.Infrastructure.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message, IDictionary<string, IEnumerable<string>> errors)
            : base(message)
        {
            this.Errors = errors;
        }
        public BadRequestException(string message, IDictionary<string, IEnumerable<string>> errors, Exception innerException)
            : base(message, innerException)
        {
            this.Errors = errors;
        }

        public IDictionary<string, IEnumerable<string>> Errors { get; }
    }
}
