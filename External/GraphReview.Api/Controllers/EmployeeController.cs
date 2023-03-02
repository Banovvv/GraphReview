using GraphReview.Application.Abstractions.Employees;
using GraphReview.Contracts.Employee;
using GraphReview.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraphReview.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(string id)
        {
            var employee = await _employeeService
                .GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var response = new EmployeeResponse()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
            };

            return Ok(response);
        }

        [HttpPost("AddEmployee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<EmployeeResponse>> AddEmployeeAsync(EmployeeRequest request)
        {
            var employee = new Employee(request.FirstName, request.LastName, request.Email);

            await _employeeService
                .AddAsync(employee);

            return CreatedAtAction(nameof(GetEmployeeByIdAsync), new { id = employee.Id });
        }
    }
}
