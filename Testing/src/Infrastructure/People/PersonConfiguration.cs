using Domain.People.Entity;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.Domain.ValueObjects;

namespace Infrastructure.People
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Person> builder)
        {
            builder.Property(c => c.BusinessId).HasConversion(c => c.Value, c => BusinessId.FromGuid(c));
        }
    }
}