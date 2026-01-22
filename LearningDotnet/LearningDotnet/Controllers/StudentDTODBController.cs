using LearningDotnet.Data;
using LearningDotnet.DependencyInjection;
using LearningDotnet.DTO;
//using LearningDotnet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LearningDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentDTODBControlle : ControllerBase
    {
        private readonly IMyLogger _logger;
        private readonly CollegeDBContext _db;

        public StudentDTODBControlle(IMyLogger logger, CollegeDBContext db)
        {
            _logger = logger;
            _db = db;
        }
        

        [HttpGet]
        //Routing
        [Route("DbAll",Name="GetAllDBStudentsStudentDTO")]
        //document the response type
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            _logger.log("student Dto called");
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
            var studentObj = _db.Students.Select(s => new StudentDTO
            {
             Id = s.Id,
            StudentName = s.StudentName,
            Email = s.Email,
            Address = s.Address,
            }
            );
            return Ok(studentObj);
                }

        //add Method for content negotiation
        [HttpGet("DBdatall")]
        public ActionResult<StudentDTO> allStudent()
        {
            var student = _db.Students.ToList();
            return Ok(student);
        }

        //[HttpGet("{id:int}")]
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentDBByIdStudentDTO")]
        //document the response type
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudent(int id) {

            //BadRequest 400 client error
            if (id <= 0 || id ==null)
              return  BadRequest();
            var student = _db.Students.Where(x => x.Id == id).FirstOrDefault();
            //NotFound 400 client error
            if (student == null)
               return NotFound($"The Student Does not Exist for this {id}");
            return Ok(student);
        }
        //[HttpGet("{name:alpha}")]
        [HttpGet()]
        [Route("{name:alpha}" ,Name = "GetStudentByNameStudentDTODB")]
        //document the response type
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudent(string name) {
            if (string.IsNullOrEmpty(name))
              return  BadRequest();
            var student = _db.Students.Where(x => x.StudentName == name).FirstOrDefault();
            //NotFound 400 client error
            if (student == null)
               return NotFound($"The Student Does not Exist for this {name}");
            return Ok(student);
        }
        ////[HttpGet("{id:int}")]
        ////[Route("{id:int}",Name = "DeleteStudentById")]
        [HttpDelete()]
        [Route("{id:int}", Name = "DeleteStudentByIdStudentDTODB")]
        //document the response type
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<bool> DeleteStudent(int id)
        {
         if (id <= 0)
              return  BadRequest();
            var student = _db.Students.Where(x => x.Id == id).FirstOrDefault();
            //NotFound 400 client error
            if (student == null)
                return NotFound($"The Student Does not Exist for this {id}");
            _db.Students.Remove(student);
            _db.SaveChanges();
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

            var studentId = _db.Students.OrderBy(x=>x.Id).LastOrDefault().Id + 1;
            Student student = new Student()
            {
                //Id= studentId,
                StudentName=studentDTO.StudentName,
                Address=studentDTO.Address,
                Email=studentDTO.Email

            };
            _db.Students.Add(student);
            _db.SaveChanges();
            // in create we have to return new Route 
            return CreatedAtRoute("GetStudentByIdStudentDTODB", new {Id= student.Id }, studentDTO);

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
            var student = _db.Students.Where(x => x.Id == studentDTO.Id).FirstOrDefault();
            if (student == null)
                return NotFound();

            student.Email = studentDTO.Email;
            student.Address = studentDTO.Address;
            student.StudentName = studentDTO.StudentName;
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartialDB")]
        //for patch need to libary
        //jsonpatch , newtonsoftjson
        public ActionResult UpdatePartial(int id,[FromBody] JsonPatchDocument<StudentDTO> studentDTO)
        {
            if (studentDTO == null && id <= 0)
            {
                return BadRequest();
            }
            var student = _db.Students.Where(x => x.Id ==id).FirstOrDefault();
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

            _db.SaveChanges();
            return NoContent();
        }
    }
}
