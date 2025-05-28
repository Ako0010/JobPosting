
using JobPosting.Models;

namespace JobPosting.DataBase;

public static class Database
{
    public static List<User> Users { get; set; } = new List<User>();
    public static List<CV> CVs { get; set; } = new List<CV>();
    public static List<JobAd> JobAds { get; set; } = new List<JobAd>();
    public static List<Application> Applications { get; set; } = new List<Application>();
}