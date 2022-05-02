using Crud.Data.Models.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crud.Data.Models.Configuration;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(x => x.Id);
        builder.SensibleRequiredString(x => x.FirstName);
        builder.SensibleRequiredString(x => x.LastName);
        builder.SensibleRequiredString(x => x.PhoneNumber);
    }
}