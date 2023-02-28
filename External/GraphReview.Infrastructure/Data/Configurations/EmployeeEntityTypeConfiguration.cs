using GraphReview.Domain.Models;
using GraphReview.Infrastructure.Data.Configurations.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphReview.Infrastructure.Data.Configurations
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> employee)
        {
            employee.Property(x => x.Id)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.IdMaxLength)
                .IsUnicode();

            employee.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.NameMaxLength)
                .IsUnicode();

            employee.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.NameMaxLength)
                .IsUnicode();

            employee.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.EmailMaxLength)
                .IsUnicode();

            employee.Property(x => x.DepartmentId)
                .IsRequired(false)
                .HasMaxLength(ConfigurationConstants.IdMaxLength)
                .IsUnicode();

            employee.HasMany(x => x.Reviews)
                .WithOne(r => r.Reviewee)
                .HasForeignKey(x => x.RevieweeId);

            employee.HasOne(x => x.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(x => x.DepartmentId);
        }
    }
}
