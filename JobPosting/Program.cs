using JobPosting.DataBase;
using JobPosting.Enum;
using JobPosting.Models;
using JobPosting.Service;
using JobPosting.Services;


var userService = new UserService();
var employerService = new EmployerService();
var jobAdService = new JobAdService();
var applicationService = new ApplicationService();
var cvService = new CVService();

Database.Users.Add(new User
{
    Id = 1,
    Name = "Employer1",
    Email = "employer1@company.com",
    Password = "12345",
    Role = UserRole.Employer
});
Database.Users.Add(new User
{
    Id = 2,
    Name = "JobSeeker1",
    Email = "job1@gmail.com",
    Password = "abcde",
    Role = UserRole.JobSeeker
});
Database.Users.Add(new User
{
    Id = 3,
    Name = "JobSeeker2",
    Email = "job2@gmail.com",
    Password = "pass2",
    Role = UserRole.JobSeeker
});

Database.JobAds.Add(new JobAd
{
    Id = 1,
    EmployerId = 1,
    Title = "Software Developer",
    Description = "Develop software applications.",
    Requirements = "C#, .NET, SQL"
});


while (true)
{
    Console.WriteLine("\n==== Main Menu ====");
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Register (Job Seeker only)");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");
    var choice = Console.ReadLine();

    if (choice == "1")
    {
        Console.Write("Email: ");
        var email = Console.ReadLine();
        Console.Write("Password: ");
        var password = Console.ReadLine();

        var user = userService.Login(new LoginModel
        {
            Email = email,
            Password = password
        });

        if (user == null)
        {
            Console.WriteLine("Login failed! Try again.");
            continue;
        }

        if (user.Role == UserRole.Employer)
        {
            EmployerMenu(user, employerService, applicationService,cvService);
        }
        else if (user.Role == UserRole.JobSeeker)
        {
            JobSeekerMenu(user, jobAdService, applicationService, cvService);
        }
    }
    else if (choice == "2")
    {
        Console.WriteLine("Register as Job Seeker");
        Console.Write("Name: ");
        var name = Console.ReadLine();
        Console.Write("Email: ");
        var email = Console.ReadLine();
        Console.Write("Password: ");
        var password = Console.ReadLine();

        userService.Register(new RegisterModel
        {
            Name = name,
            Email = email,
            Password = password
        });

        Console.WriteLine("Registration successful! You can now login.");
    }
    else if (choice == "0")
    {
        break;
    }
    else
    {
        Console.WriteLine("Invalid option.");
    }
}
    

    static void EmployerMenu(User user, EmployerService employerService, ApplicationService? applicationService,CVService? cvService)
{
    while (true)
    {
        Console.WriteLine("\n-- Employer Menu --");
        Console.WriteLine("1. Create Job Ad");
        Console.WriteLine("2. List My Job Ads");
        Console.WriteLine("3. See Applicants' CVs");
        Console.WriteLine("4. Logout");
        Console.Write("Choose an option: ");
        var choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write("Job Title: ");
            var title = Console.ReadLine();
            Console.Write("Description: ");
            var description = Console.ReadLine();
            Console.Write("Requirements: ");
            var requirements = Console.ReadLine();

            employerService.CreateJobAd(user.Id, title, description, requirements);
            Console.WriteLine("Job ad created successfully!");
        }
        else if (choice == "2")
        {
            var jobAds = employerService.GetMyJobAds(user.Id);
            Console.WriteLine("-- My Job Ads --");
            foreach (var ad in jobAds)
            {
                Console.WriteLine($"[{ad.Id}] {ad.Title} - {ad.Description}");
            }
        }
        else if (choice == "3")
        {
            var jobAds = employerService.GetMyJobAds(user.Id);
            Console.WriteLine("-- Select Job Ad ID to see applicants' CVs --");
            foreach (var ad in jobAds)
            {
                Console.WriteLine($"[{ad.Id}] {ad.Title}");
            }
            Console.Write("Job Ad ID: ");
            if (int.TryParse(Console.ReadLine(), out int jobAdId))
            {
                var applications = applicationService.GetApplicationsForJobAd(jobAdId);
                foreach (var app in applications)
                {
                    var cv = cvService.GetCVById(app.CvId);
                    if (cv != null)
                    {
                        Console.WriteLine($"Application ID: {app.Id}, CV ID: {cv.Id}, User ID: {cv.UserId}, File: {cv.FilePath}, Status: {app.Status}");
                    }
                    else
                    {
                        Console.WriteLine($"Application ID: {app.Id}, CV not found, Status: {app.Status}");
                    }

                }
                Console.Write("Application ID: ");
                if (int.TryParse(Console.ReadLine(), out int appId))
                {
                    Console.WriteLine("1. Accept");
                    Console.WriteLine("2. Reject");
                    Console.Write("Secim: ");
                    var stChoice = Console.ReadLine();
                    if (stChoice == "1")
                        applicationService.UpdateApplicationStatus(appId, ApplicationStatus.Hired);
                    else if (stChoice == "2")
                        applicationService.UpdateApplicationStatus(appId, ApplicationStatus.Rejected);
                }
            }
            else
            {
                Console.WriteLine("Invalid ID");
            }
        }
        else if (choice == "4")
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid option.");
        }
    }
}

static void JobSeekerMenu(User user, JobAdService jobAdService, ApplicationService applicationService, CVService cvService)
{
    int? uploadedCvId = null;

    while (true)
    {
        Console.WriteLine("\n-- Job Seeker Menu --");
        Console.WriteLine("1. View Job Ads");
        Console.WriteLine("2. Apply to a Job");
        Console.WriteLine("3. Upload CV");
        Console.WriteLine("4. Logout");
        Console.Write("Choose an option: ");
        var choice = Console.ReadLine();

        if (choice == "1")
        {
            var jobAds = jobAdService.GetAllJobAds();
            Console.WriteLine("-- Job Ads --");
            foreach (var ad in jobAds)
            {
                Console.WriteLine($"[{ad.Id}] {ad.Title} - {ad.Description}");
            }
        }
        else if (choice == "2")
        {
            var jobAds = jobAdService.GetAllJobAds();
            Console.WriteLine("-- Job Ads --");
            foreach (var ad in jobAds)
            {
                Console.WriteLine($"[{ad.Id}] {ad.Title} - {ad.Description}");
            }
            Console.Write("Enter Job Ad ID to apply: ");
            if (int.TryParse(Console.ReadLine(), out int jobAdId))
            {
                if (uploadedCvId == null)
                {
                    Console.WriteLine("You must upload a CV before applying!");
                }
                else
                {
                    applicationService.ApplyToJob(user.Id, jobAdId, uploadedCvId.Value);
                    Console.WriteLine("Application sent!");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID");
            }
        }
        else if (choice == "3")
        {
            Console.Write("Enter CV file path: ");
            var path = Console.ReadLine();
            var cv = cvService.UploadCV(user.Id, path);
            uploadedCvId = cv.Id;
            Console.WriteLine("CV uploaded!");
        }
        else if (choice == "4")
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid option.");
        }
    }
}