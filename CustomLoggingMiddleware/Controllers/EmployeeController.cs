using CustomLoggingMiddleware.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomLoggingMiddleware.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private static readonly List<Employee> Employees = new();

        [HttpPost("/create")]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            Employees.Add(employee);
            return Created($"/get/{employee.Id}", employee);
        }

        [HttpGet("/get/{id:int}")]
        public IActionResult GetEmployee(int id)
        {
            var foundEmployee = Employees.FirstOrDefault(e => e.Id == id);
            if (foundEmployee is null) return NotFound();
            return Ok(foundEmployee);
        }

        [HttpGet("/get/all")]
        public IActionResult GetAllEmployees()
        {
            return Ok(Employees);
        }

        [HttpPut("/update/{id:int}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            var foundEmployee = Employees.FirstOrDefault(e => e.Id == id);
            if (foundEmployee is null) return NotFound();
            foundEmployee.Position = employee.Position;
            foundEmployee.Salary = employee.Salary;
            foundEmployee.IsManager = employee.IsManager;
            return Ok(foundEmployee);
        }

        [HttpDelete("/delete/{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            var foundEmployee = Employees.FirstOrDefault(e => e.Id == id);
            if (foundEmployee is null) return NotFound();
            Employees.Remove(foundEmployee);
            return NoContent();
        }
    }
}
