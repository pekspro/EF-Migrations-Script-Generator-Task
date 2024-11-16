using Microsoft.EntityFrameworkCore;

namespace NetCore9TestApplication.Data
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

        public DbSet<SecondDataModel> SecondDataModels { get; set; } = null!;
    }

    public class SecondDataModel
    {
        public int SecondDataModelID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;
    }
}
