using Microsoft.AspNetCore.Mvc;
using Repository_Pattern.Models;
using Repository_Pattern.Services;

namespace Repository_Pattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepositry _repo;

        public EmployeeController(EmployeeRepositry repo)
        {
            _repo = repo;
        }

        // GET: api/employee/all
        [HttpGet("all")]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            var employees = await _repo.GetAllEmployee();
            return Ok(employees);
        }

        // GET: api/employee/id/5
        [HttpGet("id/{id:int}", Name = "getEmployeeById")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _repo.GetById(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // GET: api/employee/name/john
        [HttpGet("name/{name}", Name = "getEmployeeByName")]
        public async Task<ActionResult<Employee>> GetEmployeeByName(string name)
        {
            var employee = await _repo.GetByName(name);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // POST: api/employee
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmp([FromBody]Employee emp)
        {
            var createdEmployee = await _repo.CreateEmployee(emp);
            return CreatedAtRoute(
                "getEmployeeById",
                new { id = createdEmployee.Id },
                createdEmployee
            );
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmp(int id,[FromBody]Employee emp)
        {
            var createdEmployee = await _repo.UpdateEmployee(id,emp);
            return Ok(createdEmployee);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> delete(int id)
        {
            var createdEmployee = await _repo.DeleteEmployee(id);
            return Ok(createdEmployee);
        }
    }
}
