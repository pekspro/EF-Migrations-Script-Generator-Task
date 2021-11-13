using Microsoft.EntityFrameworkCore;

namespace NetCore6TestApplication.Data
{
    public class FirstDatabaseContext : DbContext
    {
        public FirstDatabaseContext(DbContextOptions<FirstDatabaseContext> options)
                : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FirstDataModel>().HasData(
                new FirstDataModel() { FirstDataModelID = 1, Name = "First name", LastName = "Last name" });
        }

        public DbSet<FirstDataModel> FirstDataModels { get; set; } = null!;
    }

    public class FirstDataModel
    {
        public int FirstDataModelID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string SecondName { get; set; } = string.Empty;

        public string ThirdName { get; set; } = string.Empty;
    }
}
