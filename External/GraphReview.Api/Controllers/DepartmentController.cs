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

        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartmentResponse>> GetDepartmentByIdAsync(string id)
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

        [HttpPost("AddDepartment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<DepartmentResponse>> AddDepartmentAsync(DepartmentRequest request)
        {
            var department = new Department(request.Name);

            await _departmentService
                .AddAsync(department);

            return CreatedAtAction(nameof(GetDepartmentByIdAsync), new { id = department.Id });
        }

        [HttpPost("AddEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddEmployeeToDepartmentAsync(AddEmployeeRequest request)
        {
            var result = await _departmentService
                .AddEmployeeAsync(request.DepartmentId, request.EmployeeId);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
