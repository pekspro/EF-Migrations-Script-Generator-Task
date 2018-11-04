using Microsoft.EntityFrameworkCore;

namespace NetCoreTestApplication.Data
{
    public class FirstDatabaseContext : DbContext
    {
        public FirstDatabaseContext(DbContextOptions<FirstDatabaseContext> options)
                : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<FirstDataModel> FirstDataModels { get; set; }
    }

    public class FirstDataModel
    {
        public int FirstDataModelID { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
    }
}
