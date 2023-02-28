namespace GraphReview.Domain.Models
{
    public class Review
    {
        public string Id { get; set; }

        public string ReviewerId { get; set; }
        public virtual Employee Reviewer { get; set; }

        public string RevieweeId { get; set; }
        public virtual Employee Reviewee { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
    }
}
