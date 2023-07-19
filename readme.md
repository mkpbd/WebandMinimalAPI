### Services

Implementation—Each service is implemented by means of a dedicated interface (or base class) to abstract the implementation.

Both the interface and the implementation can be provided by the framework, created by the developer, or acquired from a third party GitHub, NuGet packages

***Registration and configuration***—All services used by the app are configured and registered (using their interfaces) in the built-in ***IServiceProvider***class, which is a service container. The actual registration and configuration process happens within the *Program.cs* file, where the developer can also choose a suitable lifetime (***Transient, Scoped, or Singleton***).

***Dependency injection***—Each service can be injected into the constructor of the class where it’s meant to be used.
The framework, through the *IServiceProvider* container class, automatically makes available an instance of the dependency, creating a new one or possibly reusing an existing one, depending on the configured service’s lifetime, as well as disposing of it when it’s no longer needed.

IAuthorizationService, IEmailService,

## Middleware

Middleware is a set of components that operates at the HTTP level and can be used to handle the whole HTTP request processing pipeline.

middleware used in ASP.NET Core web applications include *HttpsRedirectionMiddleware*, which redirects
non-HTTPS requests to an HTTPS URL, and *AuthorizationMiddleware*, which use the authorization service internally to handle all authorization tasks at the HTTP level.

```csharp
var builder = WebApplication.CreateBuilder(args); ❶
// Add services to the container.
builder.Services.AddControllers(); ❷
// Learn more about configuring Swagger/OpenAPI
// at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); ❷
builder.Services.AddSwaggerGen(); ❷
var app = builder.Build(); ❸
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger(); ❹
app.UseSwaggerUI(); ❹
}
app.UseHttpsRedirection(); ❹
app.UseAuthorization(); ❹
app.MapControllers(); ❹
app.Run(); ❺
```

❶ Creates the WebApplicationBuilder factory class
❷ Registers and configures services
❸ Builds the WebApplication object
❹ Registers and configures nonterminal and potentially terminal middleware

❺ Registers and configures terminal middleware

By analyzing this code, we can easily see that the file is responsible for the following initialization tasks:

1. Instantiating the web application
2. Registering and configuring the services
3. Registering and configuring the middleware

Both services and middleware are registered and configured by dedicated extension methods, a convenient way to shortcut the setup process and keep the Program.cs file as concise as possible. Under ASP.NET Core naming conventions, services are configured mostly by using extension methods with the Add prefix; middleware prefixes are **Use, Map, and Run**. The only difference that concerns us, at least for now, is that the Run delegate is always 100% terminal and the last one to be processed. We’ll talk more about these conventions and their

### ***Controllers***

In ASP.NET Core, a controller is a class used to group a set of action methods (also called actions) that handle similar
HTTP requests.

From such a perspective, we could say that controllers are containers that can be used to aggregate action methods that have something in common: routing rules and prefixes, services, instances, authorization
requirements, caching strategies, HTTP-level filters, and so on.

Starting with ASP.NET Core, controllers can inherit from two built-in base classes:

**ControllerBase**, a minimal implementation without support for views

**Controller**, a more powerful implementation that inherits from **ControllerBase** and adds full support for views

```csharp
using Microsoft.AspNetCore.Mvc;
namespace MyBGList.Controllers
{
[ApiController]
public class ErrorController : ControllerBase
{
   [Route("/error")] ❶
   [HttpGet] ❷
   public IActionResult Error()
  {
   return Problem(); ❸
   }
  }
}
```

❶ The HTTP route to handle
❷ The HTTP method to handle
❸ The HTTP response to return to the caller

The Problem() method that we’re returning is a method of the ControllerBase class (which our
ErrorController extends) that produces a ProblemDetail response—a machine-readable standardized format for specifying errors in HTTP API responses based on RFC 7807

### **Cross origin Resource Sharing**

CORS is handled by checking the *Access-Control-Allow-Origin* header and ensuring that it complies with the origin of the script that issued the call. This approach is a simple request.

*Access-Control-Request-Method* —The HTTP method of the request

*Access-Control-Request-Headers* —A list of custom headers that will be sent with the request

***Origin*** —The origin of the script initiating the call

***Access-Control-Allow-Origin*** —The origin allowed to make the request (or the * wildcard if any origin is allowed). This is the same header used by simple requests

***Access-Control-Max-Age***  —How long the results of this preflight request can be cached (in seconds).

**CORS Implements**

```csharp
builder.Services.AddCors(options =>
options.AddDefaultPolicy(cfg => {
cfg.AllowAnyOrigin();
cfg.AllowAnyHeader();
cfg.AllowAnyMethod();
}));
```

Cashing Controles

Microsoft.AspNetCore.Mvc

The *[ResponseCache]* attribute is part of the Microsoft.AspNetCore.Mvc namespace. For that reason, we need to
add a reference in the Program.cs file (for Minimal APIs) and/or in the controller files where we want to use it.

```csharp
[HttpGet(Name = "GetBoardGames")]
[ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
public IEnumerable<BoardGame> Get()
```
