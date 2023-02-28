using GraphReview.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphReview.Infrastructure.Data.Configurations
{
    public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> review)
        {
            review.Property(x => x.ReviewerId)
                .IsRequired();

            review.Property(x => x.RevieweeId)
                .IsRequired();

            review.Property(x => x.StartTime)
                .IsRequired();

            review.Property(x => x.EndTime)
                .IsRequired();

            review.Property(x => x.Duration)
                .IsRequired();

            review.HasOne(x => x.Reviewer)
                .WithMany(r => r.Reviews)
                .HasForeignKey(x => x.ReviewerId);

            review.HasOne(x => x.Reviewee)
                .WithMany(r => r.Reviews)
                .HasForeignKey(x => x.RevieweeId);
        }
    }
}
