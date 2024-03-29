﻿using GraphReview.Domain.Models;
using GraphReview.Domain.Repositories;
using GraphReview.Infrastructure.Data;

namespace GraphReview.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public void AddEmployee(Department department, Employee employee)
        {
            department.Employees.Add(employee);
        }
    }
}
