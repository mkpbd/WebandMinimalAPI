using Microsoft.AspNetCore.Mvc;
using WebAPIGroupStudy34.Models;

namespace WebAPIGroupStudy34.Controllers
{
    [Route("/api/v1/Employee")]
    public class EmployeeController : ControllerBase
    {
        public List<Employee> EmployeeList { get; set; } = new List<Employee>()
        {
            new Employee()
            {
                 Id = 1,
                Name = "kamal",
                Desgination = "abc",
                Salary = 200000000
            },
            new Employee()
            {
                 Id = 2,
                Name = "tomal",
                Desgination = "bbb",
                Salary = 300000000
            }
        };

        [HttpGet]
        [Route("EmplyeeName")]
        public string EmplyeeName()
        {
            return "abc Emplyee";
        }

        [Route("GetAllEmployee")]
        [HttpGet]

        public IEnumerable<Employee> GetAllEmplyee()
        {

            return EmployeeList;

        }

        [HttpGet("{id:int}", Name ="GetSingleEmployee")]
        public  Employee GetSingleEmployee(int id) {
                var emp  = EmployeeList.Where(x => x.Id == id).FirstOrDefault();

            return emp;
        }
    }
}
