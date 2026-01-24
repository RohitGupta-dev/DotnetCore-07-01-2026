using Microsoft.AspNetCore.Mvc;
using Repository_Pattern.Models;
using Repository_Pattern.Repo;
using Repository_Pattern.Services;

namespace Repository_Pattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudnetController : ControllerBase
    {
        private readonly ICollegeRepository<student> _repo;

        public StudnetController(ICollegeRepository<student> repo)
        {
            _repo = repo;
        }

        // GET: api/student/all
        [HttpGet("StudentAll")]
        public async Task<ActionResult<List<student>>> GetStudents()
        {
            var student = await _repo.GetAll();
            return Ok(student);
        }

        // GET: api/student/id/5
        [HttpGet("id/{id:int}", Name = "getStudentById")]
        public async Task<ActionResult<student>> GetStudentById(int id)
        {
            var Student = await _repo.GetById(student=>student.Id==id,false);
            if (Student == null)
                return NotFound();

            return Ok(Student);
        }

        // GET: api/Student/name/john
        [HttpGet("name/{name}", Name = "getStudentByName")]
        public async Task<ActionResult<student>> GetStudentByName(string name)
        {
            var Student = await _repo.GetByName(student=>student.Name.ToLower().Contains(name.ToLower()));
            if (Student == null)
                return NotFound();

            return Ok(Student);
        }

        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<student>> CreateEmp([FromBody]student emp)
        {
            var createdStudent = await _repo.Create(emp);
            return CreatedAtRoute(
                "getStudentById",
                new { id = createdStudent.Id },
                createdStudent
            );
        }

        [HttpPut]
        public async Task<ActionResult<student>> UpdateEmp([FromBody] student emp)
        {
            var createdStudent = await _repo.Update(emp);
            return Ok(createdStudent);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> delete([FromBody] student emp)
        {
            var createdStudent = await _repo.Delete(emp);
            return Ok(createdStudent);
        }
    }
}
