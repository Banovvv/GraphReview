namespace GraphReview.Domain.Models
{
    public class Department
    {
        public Department(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public string? ManagerId { get; set; }
        public virtual Employee? Manager { get; set; }
    }
}