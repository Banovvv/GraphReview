namespace GraphReview.Domain.Models
{
    public class Department
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string ManagerId { get; set; }
        public virtual Employee Manager { get; set; }
    }
}