using GraphReview.Domain.Models;
using GraphReview.Infrastructure.Data.Configurations.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphReview.Infrastructure.Data.Configurations
{
    public class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> department)
        {
            department.Property(x => x.Id)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.IdMaxLength)
                .IsUnicode();

            department.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.NameMaxLength)
                .IsUnicode();

            department.HasMany(x => x.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(x => x.DepartmentId);
        }
    }
}
