using Microsoft.EntityFrameworkCore;

namespace NetCore9TestLibrary;

public class LibraryDatabaseContext : DbContext
{
    public LibraryDatabaseContext(DbContextOptions<LibraryDatabaseContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public DbSet<LibraryDatabaseDataModel> LibraryDatabaseData { get; set; } = null!;
}

public class LibraryDatabaseDataModel
{
    public int LibraryDatabaseDataModelID { get; set; }

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
