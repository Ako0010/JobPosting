using JobPosting.DataBase;
using JobPosting.Models;

namespace JobPosting.Service;

public class JobAdService
{
    public JobAd CreateJobAd(JobAd jobAd)
    {
        jobAd.Id = Database.JobAds.Count + 1;
        Database.JobAds.Add(jobAd);
        return jobAd;
    }

    public List<JobAd> GetAllJobAds()
    {
        return Database.JobAds;
    }

    public JobAd GetJobAdById(int id)
    {
        return Database.JobAds.FirstOrDefault(j => j.Id == id);
    }
}