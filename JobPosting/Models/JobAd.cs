namespace JobPosting.Models;

public class JobAd
{
    public int Id { get; set; }
    public int EmployerId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Requirements { get; set; }
}