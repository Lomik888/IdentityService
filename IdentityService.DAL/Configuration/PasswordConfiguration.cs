using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.DAL.Configuration;

public class PasswordConfiguration : IEntityTypeConfiguration<Password>
{
    public void Configure(EntityTypeBuilder<Password> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.PasswordHash).IsRequired();
    }
}