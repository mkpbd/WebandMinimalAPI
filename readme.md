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
public string Hello()
=> "[INSTANCE METHOD] Hello
World!";
}
```
