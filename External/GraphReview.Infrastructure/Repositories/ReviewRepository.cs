using GraphReview.Domain.Models;
using GraphReview.Domain.Repositories;
using GraphReview.Infrastructure.Data;

namespace GraphReview.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
