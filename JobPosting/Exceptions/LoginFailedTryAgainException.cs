
namespace JobPosting.Exceptions;

public class LoginFailedTryAgainException : Exception
{

    public LoginFailedTryAgainException() : base("Login failed! Try again.") { }
    public LoginFailedTryAgainException(string message) : base(message) { }
}
