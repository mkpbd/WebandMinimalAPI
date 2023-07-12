using Microsoft.AspNetCore.Mvc;
using WebAPIGroupStudy34.Models;

namespace WebAPIGroupStudy34.Controllers
{
    [Route("/api/v1/Employee")]
    [ApiController]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost]
        public  ActionResult<Employee>  CreateEmployee([FromForm] Employee employee)
        {

            //if (!ModelState.IsValid) return BadRequest();

            if (employee == null) return BadRequest();

            // select * from employee  ordery by id DESC
            var emp = EmployeeList.OrderByDescending(x => x.Id).FirstOrDefault();

            var lastId = emp.Id;

            Employee em = new Employee()
            {
                Id = lastId + 1,
                Name = employee.Name,
                Desgination = employee.Desgination,
                Salary = employee.Salary

            };
           EmployeeList.Add(em);

            return Ok(em);

        }

        [HttpPut("{id:int}")]
        public ActionResult<Employee> UpdateEmployee(int id ,[FromBody] Employee emp)
        {
            if (emp == null) return BadRequest();
            if (id <= 0) return BadRequest();
            var singleData = EmployeeList.OrderByDescending(x => x.Id).FirstOrDefault();

            if (singleData == null) return NoContent();

            singleData.Name = emp.Name;
            singleData.Desgination = emp.Desgination;
            singleData.Salary = emp.Salary;

            return Ok(singleData);

        }

        [HttpDelete("{id:int}")]
        public ActionResult<string> DeleteEmployee( int id)
        {
            if (id <= 0) return BadRequest();

            var singleData = EmployeeList.Where(x => x.Id == id).FirstOrDefault();

            EmployeeList.Remove(singleData);

            return Ok(singleData.Name);

        }
    }
}
