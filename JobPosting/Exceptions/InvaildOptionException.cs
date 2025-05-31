
namespace JobPosting.Exceptions;

class InvaildOptionException : Exception
{
    public InvaildOptionException() : base("Invalid option") { }
    public InvaildOptionException(string message) : base(message) { }

}
