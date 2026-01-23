using AutoMapper;
using LearningDotnet.Data;
using LearningDotnet.DependencyInjection;
using LearningDotnet.DTO;
//using LearningDotnet.Models;

//using LearningDotnet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading.Tasks;

namespace LearningDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAutoMapperController : ControllerBase
    {
        private readonly IMyLogger _logger;
        private readonly CollegeDBContext _db;
        private readonly IMapper _mapper;

        public StudentAutoMapperController(IMyLogger logger, CollegeDBContext db,IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper= mapper;
        }


        [HttpGet]
        //Routing
        [Route("DbAutoMapper", Name = "GetAllStudentByAutoMapper")]
        //document the response type
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            _logger.log("student Dto called");
            var students = await _db.Students.ToListAsync();

            var studentDto = _mapper.Map<List<StudentDTO>>(students);

            return Ok(studentDto);
        }
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentDBByIdAutoMapper")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentUsingAutoMapper(int id)
        {

            //BadRequest 400 client error
            if (id <= 0 || id == null)
                return BadRequest();
            var student = await _db.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
            var studentDto = _mapper.Map<StudentDTO>(student);
            //NotFound 400 client error
            if (studentDto == null)
                return NotFound($"The Student Does not Exist for this {id}");
            return Ok(studentDto);
        }
        //[HttpGet("{name:alpha}")]
        [HttpGet()]
        [Route("{name:alpha}", Name = "GetStudentByNameAutoMapper")]
        //document the response type
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentUsingAutoMapper(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            var student = await _db.Students.Where(x => x.StudentName == name).FirstOrDefaultAsync();
            var studentDto = _mapper.Map<StudentDTO>(student);
            //NotFound 400 client error
            if (studentDto == null)
                return NotFound($"The Student Does not Exist for this {name}");
            return Ok(studentDto);
        }
        [HttpDelete()]
        [Route("{id:int}", Name = "DeleteStudentByIdStudentAutoMapper")]
        //document the response type
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> DeleteStudentUsingAutoMapper(int id)
        {
            if (id <= 0)
                return BadRequest();
            var student = await _db.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
            //NotFound 400 client error
            if (student == null)
                return NotFound($"The Student Does not Exist for this {id}");
            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
            return Ok(true);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<StudentDTO>> PostStudentDBAutoMapper([FromBody] StudentDTO studentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Custom Error Attribute
            //1.Direct Adding
            //2.Custom Attribute
            if (studentDTO.age > 100)
            {
                ModelState.AddModelError("Age error", "Age Must Less Then 1000");
                return BadRequest();
            }

            var getStudent = await _db.Students.OrderBy(x => x.Id).LastOrDefaultAsync();
            var studentId = getStudent.Id + 1;
            Student student = new Student()
            {
                //Id= studentId,
                StudentName = studentDTO.StudentName,
                Address = studentDTO.Address,
                Email = studentDTO.Email

            };
            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();
            // in create we have to return new Route 
            return CreatedAtRoute("GetStudentDBByIdAutoMapper", new { Id = student.Id }, studentDTO);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateStudentUsingAutoMapper([FromBody] StudentDTO studentDTO)
        {
            if (studentDTO == null && studentDTO.Id <= 0)
            {
                return BadRequest();
            }
            //var student = _db.Students.Where(x => x.Id == studentDTO.Id).FirstOrDefault();
            //AsNoTracking
            var student = await _db.Students.AsNoTracking().Where(x => x.Id == studentDTO.Id).FirstOrDefaultAsync();
            if (student == null)
                return NotFound();

            var newRecord = new Student()
            {
                Id = student.Id,
                StudentName = studentDTO.StudentName,
                Address = studentDTO.Address,
                Email = studentDTO.Email,
            };
            _db.Students.Update(newRecord);
            //student.Email = studentDTO.Email;
            //student.Address = studentDTO.Address;
            //student.StudentName = studentDTO.StudentName;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartialDBAsyncAutoMapper")]
        public async Task<ActionResult> UpdatePartialUsingAutoMapper(int id, [FromBody] JsonPatchDocument<StudentDTO> studentDTO)
        {
            if (studentDTO == null && id <= 0)
            {
                return BadRequest();
            }
            var student = await _db.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (student == null)
                return NotFound();

            var std = new StudentDTO()
            {
                Id = id,
                Email = student.Email,
                Address = student.Address,
                StudentName = student.StudentName

            };
            studentDTO.ApplyTo(std, ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            student.Email = std.Email;
            student.Address = std.Address;
            student.StudentName = std.StudentName;

            await _db.SaveChangesAsync();
            return NoContent();
        }



    }
}
