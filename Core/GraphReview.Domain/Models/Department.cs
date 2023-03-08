namespace GraphReview.Domain.Models
{
    public class Department
    {
        public Department(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Employees = new List<Employee>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}