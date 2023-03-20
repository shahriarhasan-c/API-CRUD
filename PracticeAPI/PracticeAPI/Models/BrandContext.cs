using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PracticeAPI.Models
{
    public class BrandContext:DbContext
    {
        public BrandContext(DbContextOptions<BrandContext>options):base(options)
        {

        }
        public DbSet<Brand> Brands { get; set; }   
    }
}
