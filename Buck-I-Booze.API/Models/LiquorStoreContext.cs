using Microsoft.EntityFrameworkCore;

namespace BuckIBooze.API.Models
{
    public class LiquorStoreContext : DbContext
    {
        public LiquorStoreContext(DbContextOptions<LiquorStoreContext> options)
        : base(options)
        {
        }
        public DbSet<Product> Products {get; set;}
    }
}

