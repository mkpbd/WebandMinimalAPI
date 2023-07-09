**Install Web API**

    Go to Visual Studio then select Web Api  then give  a Project name  thne create button click

    my  App Name is  CollageApp

run  project

Controller

create a controller    Give  Controller name  My Controller name  StudentController

use Route Attribute  [Route("/api/[controller])")  for dynamic Controller

I have create two HTTP Varbs is called THHP Methods

```csharp
using CollageApp.Model;
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
        public IEnumerable<Student> GetAllStudents()
        {
            return new List<Student>
             {

                 new Student { Id = 1, Address="abc", Email ="mostoakamal@gmail.com", StudentName="kamal"},
                 new Student { Id = 2, Address="jamal", Email ="jamal@gmail.com", StudentName="jamal"},
             };
        }
    }
}

```
