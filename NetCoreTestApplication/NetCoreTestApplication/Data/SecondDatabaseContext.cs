using Microsoft.EntityFrameworkCore;

namespace NetCoreTestApplication.Data
{
    public class SecondDatabaseContext : DbContext
    {
        public SecondDatabaseContext(DbContextOptions<SecondDatabaseContext> options)
                : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<SecondDataModel> SecondDataModels { get; set; }
    }

    public class SecondDataModel
    {
        public int SecondDataModelID { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }
    }

}
