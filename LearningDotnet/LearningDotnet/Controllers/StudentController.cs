using LearningDotnet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All",Name="GetAllStudent")]
        public IEnumerable<Student> GetStudents()
        {
            return CollegeRepository.Students;
        }
        //[HttpGet("{id:int}")]
        [HttpGet]
        [Route("{id:int}", Name ="GetStudentById")]
        public Student GetStudent(int id) {
            return CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();
        }
        //[HttpGet("{name:alpha}")]
        [HttpGet()]
        [Route("{name:alpha}" ,Name ="GetStudentByName")]
        public Student GetStudent(string name) {
            return CollegeRepository.Students.Where(x => x.StudentName == name).FirstOrDefault();
        }
        ////[HttpGet("{id:int}")]
        ////[Route("{id:int}",Name = "DeleteStudentById")]
        [HttpDelete()]
        [Route("{id:int}", Name = "DeleteStudentById")]
        public bool DeleteStudent(int id)
        {
            var student = CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();
            CollegeRepository.Students.Remove(student);
            return true;
        }
    }
}
