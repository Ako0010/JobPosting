using JobPosting.DataBase;
using JobPosting.Models;

namespace JobPosting.Service;

public class CVService
{
    public CV UploadCV(int jobSeekerId, string filePath)
    {
        var cv = new CV
        {
            Id = Database.CVs.Count + 1,
            UserId = jobSeekerId,
            FilePath = filePath
        };
        Database.CVs.Add(cv);
        return cv;
    }

    public CV GetCVByUserId(int userId)
    {
        return Database.CVs.FirstOrDefault(c => c.UserId == userId);
    }

    public CV GetCVById(int id)
    {
        return Database.CVs.FirstOrDefault(c => c.Id == id);
    }
}