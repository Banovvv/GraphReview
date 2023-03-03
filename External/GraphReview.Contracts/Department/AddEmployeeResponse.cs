using GraphReview.Contracts.Employee;

namespace GraphReview.Contracts.Department
{
    public class AddEmployeeResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EmployeeResponse> Employees { get; set; }
    }
}
