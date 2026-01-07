using LearningDotnet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LearningDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        //Routing
        [Route("All",Name="GetAllStudentsStatus")]
        //document the response type
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(CollegeRepository.Students);
        }
        //[HttpGet("{id:int}")]
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentByIdStatus")]
        //document the response type
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> GetStudent(int id) {

            //BadRequest 400 client error
            if (id <= 0 || id ==null)
              return  BadRequest();
            var student = CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();
            //NotFound 400 client error
            if (student == null)
               return NotFound($"The Student Does not Exist for this {id}");
            return Ok(student);
        }
        //[HttpGet("{name:alpha}")]
        [HttpGet()]
        [Route("{name:alpha}" ,Name = "GetStudentByNameStatus")]
        //document the response type
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> GetStudent(string name) {
            if (string.IsNullOrEmpty(name))
              return  BadRequest();
            var student = CollegeRepository.Students.Where(x => x.StudentName == name).FirstOrDefault();
            //NotFound 400 client error
            if (student == null)
               return NotFound($"The Student Does not Exist for this {name}");
            return Ok(student);
        }
        ////[HttpGet("{id:int}")]
        ////[Route("{id:int}",Name = "DeleteStudentById")]
        [HttpDelete()]
        [Route("{id:int}", Name = "DeleteStudentByIdStatus")]
        //document the response type
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<bool> DeleteStudent(int id)
        {
         if (id <= 0)
              return  BadRequest();
            var student = CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();
            //NotFound 400 client error
            if (student == null)
                return NotFound($"The Student Does not Exist for this {id}");
            CollegeRepository.Students.Remove(student);
            return Ok(true);
        }
    }
}
