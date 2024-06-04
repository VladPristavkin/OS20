namespace FilmoSearchPortal.Domain.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException() : base("Authentication failed. Wrong user name or password.") { }

        public AuthenticationException(string message) : base(message) { }
    }
}
