using Microsoft.EntityFrameworkCore;

namespace NetCoreTestLibrary.Data
{
    public class LibraryDatabaseContext : DbContext
    {
        public LibraryDatabaseContext(DbContextOptions<LibraryDatabaseContext> options)
                : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<LibraryDatabaseDataModel> LibraryDatabaseData { get; set; }
    }

    public class LibraryDatabaseDataModel
    {
        public int LibraryDatabaseDataModelID { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
    }
}
