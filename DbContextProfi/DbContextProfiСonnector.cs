using Microsoft.EntityFrameworkCore;
using Resources.Entities;

namespace DbContextProfi
{
    public class DbContextProfiСonnector : DbContext
    {
        #region DbSets
        public DbSet<HomePageTextEntity> HomePageTexts => Set<HomePageTextEntity>();
        public DbSet<HomePageImageEntity> HomePageImages => Set<HomePageImageEntity>();
        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
        public DbSet<ServiceEntity> Services => Set<ServiceEntity>();
        public DbSet<BlogEntity> Blogs => Set<BlogEntity>();
        public DbSet<ApplicationEntity> Applications => Set<ApplicationEntity>();
        public DbSet<ContactEntity> Contacts => Set<ContactEntity>();
        #endregion

        public DbContextProfiСonnector(DbContextOptions<DbContextProfiСonnector> options) : base(options) { }
    }
}