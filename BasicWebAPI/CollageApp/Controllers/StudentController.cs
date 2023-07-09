using CollageApp.Model;
using CollageApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CollageApp.Controllers
{
    [Route("/api/[controller]")] // dynamic route use for  [controller]

    [ApiController]
    public class StudentController : ControllerBase
    {

        [HttpGet]

        public string GetStudentName()
        {
            return "Student Name : Kamal";
        }

        [HttpGet("GetAllStudents")]
        //[Route("GetAllStudent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            var result = CollegeRepository.Students;
            if (result == null || result.Count <= 0)
            {
                // 204 No Content 
                return NoContent();
            }
            
            return Ok( CollegeRepository.Students);
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<Student> GetStudentById(int id)
        {
            if(id <= 0)
            {
                // 400 Bad Request client Error 
                return BadRequest();
            }
            if(CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault() == null)
            {
                // Not found Error  404  means this data is not  find server side 
                return NotFound();
            }
            // Ok 200 reques is success and Reponse is Ok
            return Ok(CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault());
        }

        [HttpDelete("{id:int}", Name = "StudnetDelete")]
        [ProducesResponseType(200)]  // documented
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult< bool> StudnetDelete(int id)
        {
            if(id <= 0 )
            {
                // 400 BadRequest from client 
                return BadRequest();
            }
            var studnet = CollegeRepository.Students.Where(x => x.Id != id).FirstOrDefault();

            if (studnet == null)
            {
                // 404 not found  -> client site erorr
                return NotFound(false);
            }
            if (studnet != null) return Ok(true);
            else return Ok(false);
        }

        [HttpGet("GetUsers")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUsers()
        {   
            return Ok( CollegeRepository.Users);
        }
    }
}
