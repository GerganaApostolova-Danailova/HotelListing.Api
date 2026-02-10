using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace HotelListing.Api.Data;

public class HotelListingDbContext(DbContextOptions<HotelListingDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }

    public DbSet<HotelAdmin> HotelAdmins { get; set; }

    public DbSet<Booking> Bookings { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Hotel>()
       .Property(h => h.PerNightRate)
       .HasPrecision(18, 2);

        builder.Entity<Booking>()
            .Property(b => b.TotalPrice)
            .HasPrecision(18, 2);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
