namespace GraphReview.Domain.Models
{
    public class Review
    {
        public Review(DateTime startTime, int duration)
        {
            Id = Guid.NewGuid().ToString();
            Attendees = new List<Employee>();
            StartTime = startTime;
            Duration = duration;
            EndTime = StartTime.AddMinutes(Duration);
        }

        public string Id { get; set; }

        public virtual ICollection<Employee> Attendees { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
    }
}
