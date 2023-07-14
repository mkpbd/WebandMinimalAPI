## Routing

Routing is responsible for matching incoming HTTP requests and dispatching those requests to the app's executable endpoints.

 *Endpoints are the app’s units of executable request-handling code. Endpoints are defined in the app and configured when the app starts. The endpoint matching process can extract values from the request’s URL and provide those values for request processing. Using endpoint information from the app, routing is also able to generate URLs that map to endpoints.*

[Routing More Inforamtion](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-7.0)

we define the route patterns  using the Map*  methods

```csharp
app.MapGet("/hello-get", () => "[GET] Hello World!");
app.MapPost("/hello-post", () => "[POST] Hello World!");
app.MapPut("/hello-put", () => "[PUT] Hello World!");
app.MapDelete("/hello-delete", () => "[DELETE] Hello World!");
```

In this code, we have defined four endpoints, each with a different routing and method.  Of course, we can use the same route pattern with different HTTP verbs.

As soon as we add an endpoint to our application (for example, ***using MapGet()***),
*UseRouting()* is automatically added at the start of the middleware pipeline and *UseEndpoints()* at the end of the pipeline

```csharp
 // provides Map* methods for the most common HTTP verbs. If we  need to use other verbs, we can use the generic MapMethods:

            app.MapMethods("/hello-patch", new[] { HttpMethods.Patch }, () => "[PATCH] Hello World!");
            app.MapMethods("/hello-head", new[] { HttpMethods.Head }, () => "[HEAD] Hello World!");
            app.MapMethods("/hello-options", new[] { HttpMethods.Options }, () => "[OPTIONS] Hello World!");
```

In the following sections, we will show in detail how routing works effectively and how we can control its behavior.

#### Route handlers

Methods that execute when a route URL matches (according to parameters and constraints, as described) are called route handlers. **Route handlers** can be a lambda expression, a local function, an instance method, or a static method, whether synchronous or asynchronous:

Here's an example of a lambda expression (inline or using a variable):

```csharp
app.MapGet("/hello-inline", () => "[INLINE LAMBDA] Hello World!");
var handler = () => "[LAMBDA VARIABLE] Hello World!";
app.MapGet("/hello", handler);
```

Here's an example of a local function:

```csharp
string Hello() => "[LOCAL FUNCTION] Hello World!";
app.MapGet("/hello", Hello);

```

The following is an example of an instance method:

```csharp
var handlerInstance = new HelloHandler();
app.MapGet("/hello", handlerInstance.Hello);

public class HelloHandler
{
public string Hello()=> "[INSTANCE METHOD] Hello World!";
public static string HelloStatic()=> "[Static METHOD] Hello World!";
}
```

Here, we can see an example of a static method:

```csharp
 // Here, we can see an example of a static method:

 app.MapGet("/hello-static-method", HelloHandler.HelloStatic);
```

#### Route parameters

we can create route patterns with parameters that will be automatically captured by the handler

```csharp
  app.MapGet("/users/{username}/products/{productId}",
                (string username, int productId) => $"The Username is {username} and the product Id is { productId }");
```

A route can contain an arbitrary number of parameters. When a request is made to this route, the
parameters will be captured, parsed, and passed as arguments to the corresponding handler. In this
way, the handler will always receive typed arguments (in the preceding sample, we are sure that the username is string and the product ID is int).

If the route values cannot be casted to the specified types, then an exception of the **BadHttpRequestException** type will be thrown, and the API will respond with a 400 Bad *Request* message.

**Route constraints**

Route constraints are used to restrict valid types for route parameters. Typical constraints allow us
to specify that a parameter must be a number, a string, or a GUID. To specify a route constraint, we
simply need to add a colon after the parameter name, then specify the constraint name:

```csharp
app.MapGet("/users/{id:int}", (int id) => $"The user Id is {id}");
app.MapGet("/users2/{id:guid}", (Guid id) => $"The user Guid is {id}");
```

***Parameter binding***

Parameter binding is the process that converts request data (i.e., URL paths, query strings, or the
body) into strongly typed parameters that can be consumed by route handlers. ASP.NET Core minimal
APIs support the following binding sources:

1. Route values
2. Query strings
3. Headers
4. The body (as JSON, the only format supported by default)
5. A service provider (dependency injection)

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<PeopleService>();
var app = builder.Build();
app.MapPut("/people/{id:int}", (int id, bool notify, Person person, PeopleService peopleService) => { });
app.Run();
public class PeopleService { }
public record class Person(string FirstName, string
LastName);
```

Parameters that are passed to the handler are resolved in the following ways:

| Patameter     | Source                          |
| ------------- | ------------------------------- |
| Id            | Route                           |
| Notify        | Query String (case insensitive) |
| Person        | Body (as JSON )                |
| PeopleService | Service Provider                |
|               |                                 |

As we can see, ASP.NET Core is able to automatically understand where to search for parameters
for binding, based on the route pattern and the types of the parameters themselves

In this way, the API still expects a query string parameter named q, but in the handler its value is now
bound to the searchText argument.

According to the standard, the ***GET, DELETE, HEAD***, and **OPTIONS** HTTP options should never have a body. If, nevertheless, you want to use it, you need to explicitly add the **[FromBody]** attribute to the handler argument; otherwise, you’ll get an ***InvalidOperationException***  error.

**Special bindings**

it to get the context of the request and response: ***HttpContext, Request, Response***, and ***User***. In minimal APIs, we don't have a base class, but we can still access this information because it is treated as a special binding that is always available to any handler:

```csharp
app.MapGet("/products", (HttpContext context, HttpRequest req, HttpResponse res, ClaimsPrincipal user) => { });
```

We can also access all these objects using the IHttpContextAccessor interface, as we did in the previous ASP.NET Core versions

***Custom binding***

If we want to bind a parameter that comes from a route, query string, or header to a custom type, we can add a static TryParse method to the type:

```csharp
using System.Globalization;
using System.Reflection;
namespace Chapter02.Handler
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static bool TryParse(string? value, IFormatProvider? provider, out Location? location)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var values = value.Split(',', StringSplitOptions.RemoveEmptyEntries);

                if (values.Length == 2 && double.TryParse(values[0],
                NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,

out var latitude) && double.TryParse(values[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var longitude))
                {
                    location = new Location
                    {
                        Latitude = latitude,
                        Longitude = longitude
                    };
                    return true;
                }
            }
            location = null;
            return false;
        }

        public static ValueTask<Location?> BindAsync(HttpContext context, ParameterInfo parameter)
        {
            if (double.TryParse(context.Request.Query["lat"],
            NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var latitude) && double.TryParse(context.Request.Query["lon"],
            NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var longitude))
            {
                var location = new Location{ Latitude = latitude, Longitude = longitude };
                return ValueTask.FromResult<Location?>(location);
            }
            return ValueTask.FromResult<Location?>(null);
        }

    }

}

```

**Exploring responses**

we can directly return a string or a class (either synchronously or asynchronously)

If we return a string , the framework writes the string directly to the ***response***, setting its content type to **text/plain** and the status code to ***200 OK***

If we use a class, the object is serialized into the JSON format and sent to the response with the ***application/json***content type and a 200 OK status code

However, in a real application, we typically need to control the response type and the status code. In this case, we can use the static ***Results***class, which allows us to return an instance of the ***IResult***  interface, which in minimal APIs acts how ***IActionResult***does for controllers. For instance, we can use it to *return a 201 Created response* rather than a *400 Bad Request* or a *404 Not*  Found message. L et's look at some examples:

```csharp
app.MapGet("/ok", () => Results.Ok(new Person("Donald", "Duck")));
app.MapGet("/notfound", () => Results.NotFound());
app.MapPost("/badrequest", () =>
{
// Creates a 400 response with a JSON body.
    return Results.BadRequest(new { ErrorMessage = "Unable to complete the request" });
});

app.MapGet("/download", (string fileName) => Results.File(fileName));

```

We only have a few methods that allow us to explicitly set the content type, when getting a file with **Results.Bytes(),** **Results.Stream(), and Results.File(),** or when using **Results.Text()** and **Results.Content().** In all other cases, when we're dealing with complex objects, the response will be in JSON format. This is a precise design choice since most developers rarely need to support other media types. By supporting only JSON without performing content negotiation, minimal APIs can be very efficient
