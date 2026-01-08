using LearningDotnet.DTO;
using LearningDotnet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Converters;
using Microsoft.AspNetCore.Mvc;

namespace LearningDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentDTOController : ControllerBase
    {
        [HttpGet]
        //Routing
        [Route("All",Name="GetAllStudentsStudentDTO")]
        //document the response type
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            var students= new List<StudentDTO>();

            //foreach (var item in CollegeRepository.Students)
            //{
            //    StudentDTO StudentObj = new StudentDTO()
            //    {
            //        Id = item.Id,
            //        StudentName = item.StudentName,
            //        Email = item.Email,
            //        Address = item.Address,
            //    };
            //    StudentObj.Status = StudentObj.Status;
            //    students.Add(StudentObj);
            //}
            var studentObj = CollegeRepository.Students.Select(s => new StudentDTO
            {
             Id = s.Id,
            StudentName = s.StudentName,
            Email = s.Email,
            Address = s.Address,
            }
            );
            return Ok(studentObj);
        }
        //[HttpGet("{id:int}")]
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentByIdStudentDTO")]
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
        [Route("{name:alpha}" ,Name = "GetStudentByNameStudentDTO")]
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
        [Route("{id:int}", Name = "DeleteStudentByIdStudentDTO")]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<StudentDTO> PostStudent([FromBody]StudentDTO studentDTO) { 
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            //Custom Error Attribute
            //1.Direct Adding
            //2.Custom Attribute
            if (studentDTO.age > 100)
            {
                ModelState.AddModelError("Age error","Age Must Less Then 1000");
                return BadRequest();
            }

            var studentId = CollegeRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student()
            {
                Id= studentId,
                StudentName=studentDTO.StudentName,
                Address=studentDTO.Address,
                Email=studentDTO.Email

            };
            CollegeRepository.Students.Add(student);
            // in create we have to return new Route 
           return CreatedAtRoute("GetStudentByIdStudentDTO", new {Id= student.Id }, studentDTO);

            //ok is for genral sucess 
           // // return Ok(student);
        }

        [HttpPut]
        public ActionResult UpdateStudent([FromBody] StudentDTO studentDTO)
        {
            if(studentDTO == null && studentDTO.Id <= 0)
            {
                return BadRequest();
            }
            var student = CollegeRepository.Students.Where(x => x.Id == studentDTO.Id).FirstOrDefault();
            if (student == null)
                return NotFound();

            student.Email = studentDTO.Email;
            student.Address = studentDTO.Address;
            student.StudentName = studentDTO.StudentName;

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        //for patch need to libary
        //jsonpatch , newtonsoftjson
        public ActionResult UpdatePartial(int id,[FromBody] JsonPatchDocument<StudentDTO> studentDTO)
        {
            if (studentDTO == null && id <= 0)
            {
                return BadRequest();
            }
            var student = CollegeRepository.Students.Where(x => x.Id ==id).FirstOrDefault();
            if (student == null)
                return NotFound();

            var std = new StudentDTO()
            {
                Id=id,
                Email= student.Email,
                Address= student.Address,
                StudentName= student.StudentName

            };
            studentDTO.ApplyTo(std,ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            student.Email = std.Email;
            student.Address = std.Address;
            student.StudentName = std.StudentName;

            return NoContent();
        }
    }
}
