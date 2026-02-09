using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Api.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole { Id = "6bab5d5b-212f-4aba-a446-112043609d2f", Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
            new IdentityRole { Id = "c6a37278-57ba-4d2d-a4cd-8bb9a6a99c98", Name = "User", NormalizedName = "USER" }
        );
    }
}
