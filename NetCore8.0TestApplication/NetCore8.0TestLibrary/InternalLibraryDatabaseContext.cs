using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NetCore8.0TestApplication")]

namespace NetCore8TestLibrary;


internal class InternalLibraryDatabaseContext : DbContext
{
    public InternalLibraryDatabaseContext(DbContextOptions<InternalLibraryDatabaseContext> options)
            : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public DbSet<LibraryDatabaseDataModel> LibraryDatabaseData { get; set; } = null!;
}
