using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NetCoreTestApplication")]

namespace NetCoreTestLibrary.Data
{
    internal class InternalLibraryDatabaseContext : DbContext
    {
        public InternalLibraryDatabaseContext(DbContextOptions<InternalLibraryDatabaseContext> options)
                : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<LibraryDatabaseDataModel> LibraryDatabaseData { get; set; }
    }
}
