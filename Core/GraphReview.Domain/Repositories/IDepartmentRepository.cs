using GraphReview.Domain.Models;

namespace GraphReview.Domain.Repositories
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        void AddEmployee(Department department, Employee employee);
    }
}
