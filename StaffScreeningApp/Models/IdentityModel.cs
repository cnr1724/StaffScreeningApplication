using Microsoft.EntityFrameworkCore;

namespace StaffScreeningApp.Models
{
    public class IdentityModel : DbContext
    {
        public IdentityModel(DbContextOptions<IdentityModel> options) : base(options)
        {
        }
        public DbSet<Login> login { get; set; }
        public DbSet<Staffscreening> Staffscreenings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staffscreening>().HasKey(e=>e.user_id);
        }
    }
}
