﻿using GraphReview.Domain.Models;
using GraphReview.Infrastructure.Data.Configurations.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphReview.Infrastructure.Data.Configurations
{
    public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> review)
        {
            review.Property(x => x.Id)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.IdMaxLength)
                .IsUnicode();

            review.Property(x => x.StartTime)
                .IsRequired();

            review.Property(x => x.EndTime)
                .IsRequired();

            review.Property(x => x.Duration)
                .IsRequired();

            review.HasMany(x => x.Attendees)
                .WithMany(r => r.Reviews);
        }
    }
}
