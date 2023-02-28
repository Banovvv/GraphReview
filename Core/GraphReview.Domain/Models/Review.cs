namespace GraphReview.Domain.Models
{
    public class Review
    {
        public Review(string reviewerId, string revieweeId, DateTime startTime, int duration)
        {
            Id = Guid.NewGuid().ToString();
            ReviewerId = reviewerId;
            RevieweeId = revieweeId;
            StartTime = startTime;
            Duration = duration;
            EndTime = StartTime.AddMinutes(Duration);
        }

        public string Id { get; set; }

        public string? ReviewerId { get; set; }
        public virtual Employee? Reviewer { get; set; }

        public string? RevieweeId { get; set; }
        public virtual Employee? Reviewee { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
    }
}
