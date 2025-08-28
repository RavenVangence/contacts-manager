using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GeniusContactManager.Data
{
    public class ContactContextFactory : IDesignTimeDbContextFactory<ContactContext>
    {
        public ContactContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContactContext>();
            optionsBuilder.UseSqlServer(@"Server=CYB_LODRICKM;Database=GeniusDB;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True");

            return new ContactContext(optionsBuilder.Options);
        }
    }
}
