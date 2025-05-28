using JobPosting.DataBase;
using JobPosting.Models;

namespace JobPosting.Services
{
    public class EmployerService
    {
        public JobAd CreateJobAd(int employerId, string title, string description, string requirements)
        {
            var jobAd = new JobAd
            {
                Id = Database.JobAds.Count + 1,
                EmployerId = employerId,
                Title = title,
                Description = description,
                Requirements = requirements
            };
            Database.JobAds.Add(jobAd);
            return jobAd;
        }

        public List<JobAd> GetMyJobAds(int employerId)
        {
            var result = new List<JobAd>();
            foreach (var ad in Database.JobAds)
            {
                if (ad.EmployerId == employerId)
                {
                    result.Add(ad);
                }
            }
            return result;
        }

        public List<CV> GetApplicantsCVs(int jobAdId)
        {
            List<int> cvIds = new List<int>();
            foreach (var application in Database.Applications)
            {
                bool alreadyAdded = false;
                foreach (var id in cvIds)
                {
                    if (id == application.CvId)
                    {
                        alreadyAdded = true;
                        break;
                    }
                }
                if (application.JobAdId == jobAdId && !alreadyAdded)
                {
                    cvIds.Add(application.CvId);
                }
            }

            List<CV> result = new List<CV>();
            foreach (var cv in Database.CVs)
            {
                bool found = false;
                foreach (var id in cvIds)
                {
                    if (cv.Id == id)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    result.Add(cv);
                }
            }
            return result;
        }
    }
}