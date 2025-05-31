

namespace JobPosting.Exceptions
{
    class InvalidIdException : Exception
    {
        public InvalidIdException() : base("Invalid ID") { }
        public InvalidIdException(string message) : base(message) { }
    }
}
