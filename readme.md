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

| **Constraint** | **Description**                                                              | **Example**              |
| -------------------- | ---------------------------------------------------------------------------------- | ------------------------------ |
| alpha                | Matches uppercase or lowercase Latin alphabet characters (a-z, A-Z)                | {x:alpha}                      |
| -                    | -                                                                                  | -                              |
| bool                 | Matches a Boolean value.                                                           | {x:bool}                       |
| datetime             | Matches a DateTime value.                                                          | {x:datetime}                   |
| decimal              | Matches a decimal value.                                                           | {x:decimal}                    |
| double               | Matches a 64-bit floating-point value.                                             | {x:double}                     |
| float                | Matches a 32-bit floating-point value.                                             | {x:float}                      |
| guid                 | Matches a GUID value.                                                              | {x:guid}                       |
| int                  | Matches a 32-bit integer value.                                                    | {x:int}                        |
| length               | Matches a string with the specified length or within a specified range of lengths. | {x:length(6)} {x:length(1,20)} |
| long                 | Matches a 64-bit integer value.                                                    | {x:long}                       |
| max                  | Matches an integer with a maximum value.                                           | {x:max(10)}                    |
| maxlength            | Matches a string with a maximum length.                                            | {x:maxlength(10)}              |
| min                  | Matches an integer with a minimum value.                                           | {x:min(10)}                    |
| minlength            | Matches a string with a minimum length.                                            | {x:minlength(10)}              |
| range                | Matches an integer within a range of values.                                       | {x:range(10,50)}               |
| regex                | Matches a regular expression.                                                      | {x:regex(^\d{3}-\d{3}-\d{4}$)} |

## Return Types

ASP.NET Core Web API allows us to return different types from the controller action methods:

* Specific Types
* IActionResult
* ActionResult `<T>`

## Specific Return Types

In its simplest form, an ASP.NET Core Web API controller action can just return a specific type like a string or a custom entity. Let’s consider a simple controller action method that returns the list of all employees:

```csharp
  [HttpGet("GetAllStudents")]
        //[Route("GetAllStudent")]
        public List<Student> GetAllStudents()
        {
            return CollegeRepository.Students;
        }
```

This action will return a**200 Ok** status code along with a collection of `Employee` objects when it runs successfully. Of course, in case of errors, it will return a **500 Error** status code along with error details. it will generate this possible successful outcome in the **Responses** section

However, if we want to add some validations into the action and return a validation failure with a**400 Bad Request** response, this approach won’t work and we have to use either `IActionResult` or `ActionResult<T>` types. We will explain how to do that in later sections.

Since the action doesn’t have any validations or multiple return paths, returning a specific type work well here.

### IEnumerable `<T>` vs IAsyncEnumerable `<T>`

It is a common practice to return a collection from controller actions using the `IEnumerable<T>` type. However, there is an important behavior of ASP.NET Core that we need to consider before choosing this type.  ASP.NET Core buffers the result of the action endpoint that returns `IEnumerable<T>` before writing them into the response. This means even if we get the underlying data part by part asynchronously, ASP.NET Core will wait till it receives the complete data and then send the response at once.

```csharp
      [HttpGet("GetAllStudents")]
        //[Route("GetAllStudent")]
        public IEnumerable<Student> GetAllStudents()
        {
            return CollegeRepository.Students;
        }
```

For instance, let's inspect an action method that uses the `yield return` statement to return elements one at a time:

```csharp
[HttpGet("active")]
public IEnumerable<Employee> GetActive()
{
    foreach (var employee in _repository.GetActiveEmployees())
    {
        yield return employee;
    }                 
}
```

Here, even if the repository supports returning data part by part asynchronously, our action endpoint will still wait till it receives all the data and then returns everything together.

So what if we want to support asynchronous iteration? Well, for that, we need to use the `IAsynEnumerable<T>` with the `await foreach` syntax:

```csharp
[HttpGet("activeasync")]
public async IAsyncEnumerable<Employee> GetActiveAsync()
{
    await foreach (var employee in _repository.GetActiveEmployeesAsync())
    {
        yield return employee;
    }
}
```

We have explained the concept of `IAsyncEnumerable` in detail in our [IAsyncEnumerable with yield in C#](https://code-maze.com/csharp-async-enumerable-yield/) article and it will be a good reference to learn more about this topic.

## IActionResult Return Type

Whenever an action has multiple return paths and needs to support returning multiple `ActionResult` types, then  `IActionResult` is a great choice. Several types derive from the `ActionResult` type and each of them represents an HTTP status code. For instance, when we want to return a **400 Bad Request** response, we can use the `BadRequestResult` type. Similarly, we can use `NotFoundResult` for returning a **404 Not Found** response and `OkObjectResult` for returning a **200 Ok** response.

On top of that, the `ControllerBase` class has defined some convenience methods which is equivalent to creating and returning an `ActionResult` type. For instance, if we want to return a **400 Bad Request** response, instead of using the `return new BadRequestResult();`, we could just write `return BadRequest();` . Similarly, we could use `Ok()` and `NotFound()` methods to return the **200 Ok** and **404 Not Found** responses respectively.

To see this in action, first, let’s create a synchronous action method for fetching an employee that returns the `IActionResult` return type:

```csharp
[HttpGet("{id}")]
[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public IActionResult GetById(int id)
{
    if (!_repository.TryGetEmployee(id, out Employee? employee))
    {
        return NotFound();
    }

    return Ok(employee);
}
```

Also, note how we have specified the `[ProducesResponseType]` attribute multiple times to indicate all possible response status codes and types.

Now let’s create an asynchronous action method for creating an employee record that returns the `IActionResult` type:

```csharp
[HttpPost]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<IActionResult> CreateAsync([FromBody] Employee employee)
{
    if (employee.Name.Length < 3 || employee.Name.Length > 30)
    {
        return BadRequest("Name should be between 3 and 30 characters.");
    }

    await _repository.AddEmployeeAsync(employee);

    return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
}
```

We have two possible return types in this action as well. In case the validation rule fails on the employee name field, it returns a **400 Bad Request** response. On the other hand, once the employee record is created successfully, it returns a **201 Created Success** status code. Note that we are using the `CreatedAtAction()` shorthand method which will return the newly created employee record along with the response.

### Automatic HTTP 400 Response

While using ASP.NET Core Web API, if we mark a controller with the `[ApiController]`  attribute, it will automatically trigger an **HTTP 400** response if there is a model validation error.

For instance, let’s say we have marked some of the attributes of our `Employee` class with the `[Required]` attribute:

```csharp
public class Employee
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public bool IsActive { get; set; }
}
```

Now if we do not provide a value for the `Name` property in the request, it will automatically return a **400 Bad request** response provided the `[ApiController]`  attribute is applied to the `EmployeeController` class. [ApiController attribute article](https://code-maze.com/apicontroller-attribute-in-asp-net-core-web-api/).

## ActionResult `<T>` Return Type

ASP.NET Core supports returning the `ActionResult<T>` type from Web API controller actions. While using the `ActionResult<T>`, we can either return an `ActionResult` type or a specific type.

One advantage of using this type is that we can skip the `Type` property of the `[ProducesResponseType]` attribute. This is because the expected return type can be inferred from the `T` in the `ActionResult<T>`.

Similarly, while returning the `ActionResult<T>` type, it supports implicit casting of both type `T` and `ActionResult` to `ActionResult<T>`. This means instead of writing `return new OkObjectResult(employee)` we could simply write `return employee` and it will give the same results.

However, remember that C# doesn’t support implicit casting on interfaces. For instance, if we are using `ActionResult<IEnumerable<T>>` as the return type on an action,  we cannot return an `IEnumerable<T>` type as the implicit casting will not work in that case. One solution for this is to convert the `IEnumerable` collection to a `List` using the `.ToList()` method before returning it.

```csharp
[HttpGet("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public ActionResult<Employee> GetById(int id)
{
    if (!_repository.TryGetEmployee(id, out var employee))
    {
        return NotFound();
    }               
    return employee;
}
```

Note that we have removed the `Type` property from the `[ProducesResponseType]` attribute. Apart from that, we have modified the return type to `ActionResult<Employee>` and we return the `employee` object directly. If the employee record is not found, it still returns the 404 `ActionResult` response. Here ASP.NET Core will cast both the `ActionResult` and the `Employee` type to the `ActionResult<Employee>` type.

```csharp
[HttpPost]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<Employee>> CreateAsync(Employee employee)
{
    if (employee.Name.Length < 3 || employee.Name.Length > 30)
    {
        return BadRequest("Name should be between 3 and 30 characters.");
    }
    await _repository.AddEmployeeAsync(employee);
    return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
}
```

Here, we just need to change the return type from `IActionResult` to `ActionResult<Employee>` . Since this action returns `ActionResult` type responses in both cases, ASP.NET Core will cast it to `ActionResult<Employee>` type
