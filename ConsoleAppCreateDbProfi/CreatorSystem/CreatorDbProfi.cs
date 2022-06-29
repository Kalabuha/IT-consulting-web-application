using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using DbContextProfi;
using Resources.Entities;
using ConsoleAppCreateDbProfi.TestData.TestEntities;

namespace ConsoleAppCreateDbProfi.CreatorSystem
{
    internal class CreatorDbProfi
    {
        private readonly DbContextProfiСonnector _context;

        private readonly string _directory;

        public CreatorDbProfi(DbContextProfiСonnector context)
        {
            _context = context;
            _directory = @"..\..\..\TestData\";
        }

        public void CreateDbProfi()
        {
            _context.Database.Migrate();

            FillProjectsTable(@"projects.json");
            FillServicesTable(@"services.json");
            FillBlogsTable(@"blogs.json");
            FillTextsTable(@"texts.json");
            FillImagesTable(@"images.json");
            FillContactsTable(@"contacts.json");

            _context.SaveChanges();
        }

        private void FillProjectsTable(string projectsJsonFileName)
        {
            var projectsJsonPath = Path.Combine(_directory, projectsJsonFileName);

            if (!File.Exists(projectsJsonPath))
            {
                return;
            }

            var projectsJson = File.ReadAllText(projectsJsonPath);
            var projects = JsonSerializer.Deserialize<List<ProjectTestEntity>>(projectsJson);

            if (projects == null)
            {
                return;
            }

            foreach (var project in projects)
            {
                _context.Projects.Add(new ProjectEntity
                {
                    Title = project.Title,
                    ProjectDescription = project.Description,
                    LinkToCustomerSite = project.Link,
                    CustomerCompanyLogoAsArray64 = Convert.FromBase64String(project.Image),
                    IsPublished = true
                });
            }
        }

        private void FillServicesTable(string servicesJsonFileName)
        {
            var servicesJsonPath = Path.Combine(_directory, servicesJsonFileName);

            if (!File.Exists(servicesJsonPath))
            {
                return;
            }

            var servicesJson = File.ReadAllText(servicesJsonPath);
            var services = JsonSerializer.Deserialize<List<ServiceTestEntity>>(servicesJson);

            if (services == null)
            {
                return;
            }

            foreach (var service in services)
            {
                _context.Services.Add(new ServiceEntity
                {
                    Title = service.Title,
                    ServiceDescription = service.Description,
                    IsPublished = true
                });
            }
        }

        private void FillBlogsTable(string blogsJsonFileName)
        {
            var blogsJsonPath = Path.Combine(_directory, blogsJsonFileName);

            if (!File.Exists(blogsJsonPath))
            {
                return;
            }

            var blogsJson = File.ReadAllText(blogsJsonPath);
            var blogs = JsonSerializer.Deserialize<List<BlogTestEntity>>(blogsJson);

            if (blogs == null)
            {
                return;
            }

            foreach (var blog in blogs)
            {
                _context.Blogs.Add(new BlogEntity
                {
                    Title = blog.Title,
                    ShortBlogDescription = blog.ShortDescription,
                    LongBlogDescription = blog.LongDescription,
                    BlogImageAsArray64 = Convert.FromBase64String(blog.BlogImage),
                    PublicationDate = blog.Publication,
                    IsPublished = true,
                });
            }
        }

        private void FillTextsTable(string textsJsonFileName)
        {
            var textsJsonPath = Path.Combine(_directory, textsJsonFileName);

            if (!File.Exists(textsJsonPath))
            {
                return;
            }

            var textsJson = File.ReadAllText(textsJsonPath);
            var texts = JsonSerializer.Deserialize<List<TextTestEntity>>(textsJson);

            if (texts == null)
            {
                return;
            }

            foreach (var text in texts)
            {
                _context.HomePageTexts.Add(new HomePageTextEntity
                {
                    Text = text.Text,
                    IsPostedOnThePage = text.IsPosted
                });
            }
        }

        private void FillImagesTable(string imageJsonFileName)
        {
            var imageJsonPath = Path.Combine(_directory, imageJsonFileName);

            if (!File.Exists(imageJsonPath))
            {
                return;
            }

            var imagesJson = File.ReadAllText(imageJsonPath);
            var images = JsonSerializer.Deserialize<List<ImageTestEntity>>(imagesJson);

            if (images == null)
            {
                return;
            }

            foreach (var image in images)
            {
                _context.HomePageImages.Add(new HomePageImageEntity
                {
                    ImageAsArray64 = Convert.FromBase64String(image.Image),
                    IsPostedOnThePage = image.IsPosted
                });
            }
        }

        private void FillContactsTable(string contactsJsonFileName)
        {
            var contactsJsonPath = Path.Combine(_directory, contactsJsonFileName);

            if (!File.Exists(contactsJsonPath))
            {
                return;
            }

            var contactsJson = File.ReadAllText(contactsJsonPath);
            var contacts = JsonSerializer.Deserialize<List<ContactTestEntity>>(contactsJson);

            if (contacts == null)
            {
                return;
            }

            foreach (var contact in contacts)
            {
                _context.Contacts.Add(new ContactEntity
                {
                    Address = contact.Address,
                    MapAsArray64 = Convert.FromBase64String(contact.Map),
                    Phone = contact.Phone,
                    Fax = contact.Fax,
                    IsPostedOnThePage = contact.IsPosted,
                    Postcode = contact.Postcode,
                });
            }
        }
    }
}
