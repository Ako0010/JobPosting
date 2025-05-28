using JobPosting.DataBase;
using JobPosting.Enum;
using JobPosting.Models;

namespace JobPosting.Services
{
    public class ApplicationService
    {
        public void ApplyToJob(int jobSeekerId, int jobAdId, int cvId)
        {
            var application = new Application
            {
                Id = Database.Applications.Count + 1,
                JobAdId = jobAdId,
                JobSeekerId = jobSeekerId,
                CvId = cvId,
                Status = ApplicationStatus.Pending
            };
            Database.Applications.Add(application);
        }

        public void UpdateApplicationStatus(int applicationId, ApplicationStatus status)
        {
            foreach (var app in Database.Applications)
            {
                if (app.Id == applicationId)
                {
                    app.Status = status;
                    break;
                }
            }
        }

        public List<Application> GetApplicationsForJobAd(int jobAdId)
        {
            List<Application> result = new List<Application>();
            foreach (var application in Database.Applications)
            {
                if (application.JobAdId == jobAdId)
                {
                    result.Add(application);
                }
            }
            return result;
        }
    }
}