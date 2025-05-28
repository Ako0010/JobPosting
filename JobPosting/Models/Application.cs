using JobPosting.Enum;

namespace JobPosting.Models;

public class Application
{
    public int Id { get; set; }
    public int JobAdId { get; set; }
    public int IwciId { get; set; }
    public int CvId { get; set; }
    public ApplicationStatus Status { get; set; }
    public int JobSeekerId { get; internal set; }
}