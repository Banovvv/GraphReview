﻿namespace GraphReview.Domain.Models
{
    public class Employee
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}