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
        public IEnumerable<Student> GetAllStudents()
        {
            return CollegeRepository.Students;
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        public Student GetStudentById(int id)
        {
            return CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpDelete("{id:int}", Name = "StudnetDelete")]
        public bool StudnetDelete(int id)
        {
            var studnet = CollegeRepository.Students.Where(x => x.Id != id).FirstOrDefault();

            if (studnet != null) return true;
            else return false;
        }
    }
}
