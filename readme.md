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

we can not use  Student information in Controller we use a Repository  to get Studnet information as property

I have Create  ColleageRepository and   put Student information

```csharp
using CollageApp.Model;

namespace CollageApp.Repository
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>
             {

                 new Student { Id = 1, Address="abc", Email ="mostoakamal@gmail.com", StudentName="kamal"},
                 new Student { Id = 2, Address="jamal", Email ="jamal@gmail.com", StudentName="jamal"},
             };
    }
}

```

Now our  Student Controller is Clean

```csharp
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
        public IEnumerable<Student> GetAllStudents()
        {
            return CollegeRepository.Students;
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        public Student GetStudentById(int id)
        {
            return CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}

```

I have also create  a HttpGet  API and Get Single student info  By Id

```csharp
    [HttpGet("{id:int}", Name = "GetStudentById")]
        public Student GetStudentById(int id)
        {
            return CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();
        }
```

 Delete Http Method  

```csharp
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

```
