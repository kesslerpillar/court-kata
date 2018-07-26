using Microsoft.EntityFrameworkCore;

namespace CourtApi.com.pillartechnology.court
{
    public interface Persistable
    {
        DbSet<Case> Cases { get; set; }
        int SaveChanges();
    }
}