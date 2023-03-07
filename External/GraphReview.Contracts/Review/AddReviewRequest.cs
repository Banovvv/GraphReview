namespace GraphReview.Contracts.Review
{
    public class AddReviewRequest
    {
        public List<string> AttendeeIds { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
    }
}
