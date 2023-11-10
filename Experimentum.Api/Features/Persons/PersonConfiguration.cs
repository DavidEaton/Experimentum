using Experimentum.Api.Shared;
using Experimentum.Domain.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Experimentum.Api.Features.Persons
{
    public class PersonConfiguration : EntityConfiguration<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            base.Configure(builder);
            builder.ToTable("Person", "dbo");

            // Value Object: Name
            builder.OwnsOne(person => person.Name)
                   .Property(personName => personName.FirstName)
                   .HasColumnName("FirstName")
                   .IsRequired()
                   .HasMaxLength(255);
            builder.OwnsOne(person => person.Name)
                   .Property(personName => personName.LastName)
                   .HasColumnName("LastName")
                   .IsRequired().HasMaxLength(255);
            builder.OwnsOne(person => person.Name)
                   .Property(personName => personName.MiddleName)
                   .HasColumnName("MiddleName")
                   .HasMaxLength(255);

            builder.OwnsOne(person => person.Email)
                   .Property(email => email.Address)
                   .HasColumnName("EmailAddress")
                   .IsRequired()
                   .HasMaxLength(255);
        }
    }
}
