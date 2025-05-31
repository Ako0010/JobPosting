

namespace JobPosting.Exceptions;

class CvNotFoundException : Exception
{
    public CvNotFoundException() : base("CV not found") { }
    public CvNotFoundException(string message) : base(message)
    {
    }
}