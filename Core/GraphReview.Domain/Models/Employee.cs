namespace GraphReview.Domain.Models
{
    public class Employee
    {
        public Employee(string firstName, string lastName, string email)
        {
            Id = Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
    }
}
