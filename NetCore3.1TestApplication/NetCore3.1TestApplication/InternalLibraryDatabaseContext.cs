using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NetCore3.1TestApplication")]

namespace NetCore3TestLibrary
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
