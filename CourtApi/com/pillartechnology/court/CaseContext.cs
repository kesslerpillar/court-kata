using Microsoft.EntityFrameworkCore;

namespace CourtApi.com.pillartechnology.court
{

    public class CaseContext : DbContext
    {
        public CaseContext(DbContextOptions<CaseContext> options)
            : base(options)
        {
        }
 
        public DbSet<Case> Cases { get; set; }
    }
}