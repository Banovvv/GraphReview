using GraphReview.Application.Abstractions.Departments;
using GraphReview.Contracts.Department;
using GraphReview.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraphReview.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartmentResponse>> GetDepartmentById(string id)
        {
            var department = await _departmentService
                .GetByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            var response = new DepartmentResponse()
            {
                Id = department.Id,
                Name = department.Name
            };

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<DepartmentResponse>> CreateEmployee(DepartmentRequest request)
        {
            var department = new Department(request.Name);

            await _departmentService
                .AddAsync(department);

            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id });
        }
    }
}
