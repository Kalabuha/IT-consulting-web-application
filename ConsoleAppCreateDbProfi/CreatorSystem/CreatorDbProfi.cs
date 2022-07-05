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

        private readonly string _FilesDdirectory;

        public CreatorDbProfi(DbContextProfiСonnector context)
        {
            _context = context;
            _FilesDdirectory = @"..\..\..\TestData\";
        }

        public void CreateDbProfi()
        {
            _context.Database.Migrate();

            FillProjectsTable("projects.json");
            FillServicesTable("services.json");
            FillBlogsTable("blogs.json");
            FillTextsTable("texts.json");
            FillImagesTable("images.json");
            FillContactsTable("contacts.json");

            _context.SaveChanges();
        }

        private TEntityTest[] GetTestEntities<TEntityTest>(string nameJsonFile) where TEntityTest : class
        {
            var pathJsonFile = Path.Combine(_FilesDdirectory, nameJsonFile);

            if (!File.Exists(pathJsonFile))
            {
                return new TEntityTest[0];
            }

            var entitiesJson = File.ReadAllText(pathJsonFile);
            var testEntities = JsonSerializer.Deserialize<TEntityTest[]>(entitiesJson);

            return testEntities ?? new TEntityTest[0];
        }

        private void FillProjectsTable(string projectsNameJsonFile)
        {
            var projects = GetTestEntities<ProjectTestEntity>(projectsNameJsonFile);

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

        private void FillServicesTable(string servicesNameJsonFile)
        {
            var services = GetTestEntities<ServiceTestEntity>(servicesNameJsonFile);

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

        private void FillBlogsTable(string blogsNameJsonFile)
        {
            var blogs = GetTestEntities<BlogTestEntity>(blogsNameJsonFile);

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

        private void FillTextsTable(string textsNameJsonFile)
        {
            var texts = GetTestEntities<TextTestEntity>(textsNameJsonFile);

            foreach (var text in texts)
            {
                _context.HomePageTexts.Add(new HomePageTextEntity
                {
                    Text = text.Text,
                    IsPostedOnThePage = text.IsPosted
                });
            }
        }

        private void FillImagesTable(string imagesNameJsonFile)
        {
            var images = GetTestEntities<ImageTestEntity>(imagesNameJsonFile);

            foreach (var image in images)
            {
                _context.HomePageImages.Add(new HomePageImageEntity
                {
                    ImageAsArray64 = Convert.FromBase64String(image.Image),
                    IsPostedOnThePage = image.IsPosted
                });
            }
        }

        private void FillContactsTable(string contactsNameJsonFile)
        {
            var contacts = GetTestEntities<ContactTestEntity>(contactsNameJsonFile);

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
