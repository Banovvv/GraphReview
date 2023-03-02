using GraphReview.Domain.Models;
using GraphReview.Domain.Repositories;
using GraphReview.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphReview.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllByDepartmentAsync(string departmentId, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(x => x.DepartmentId == departmentId)
                .ToListAsync(cancellationToken);
        }
    }
}
