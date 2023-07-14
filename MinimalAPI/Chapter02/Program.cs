
using Chapter02.Handler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Xml.Linq;

namespace Chapter02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddScoped<PeopleHandler>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/hello-get", () => "[GET] Hello World!");

            app.MapPost("/hello-post", () => "[POST] Hello World!");

            app.MapPut("/hello-put", () => "[PUT] Hello World!");

            app.MapDelete("/hello-delete", () => "[DELETE] Hello    World!");

            // provides Map* methods for the most common HTTP verbs. If we  need to use other verbs, we can use the generic MapMethods:

            app.MapMethods("/hello-patch", new[] { HttpMethods.Patch }, () => "[PATCH] Hello World!");
            app.MapMethods("/hello-head", new[] { HttpMethods.Head }, () => "[HEAD] Hello World!");
            app.MapMethods("/hello-options", new[] { HttpMethods.Options }, () => "[OPTIONS] Hello World!");

            // Handlers route 
            // Here’s an example of a lambda expression (inline or using a variable):
            var handler = () => "[LAMBDA VARIABLE] Hello World!";
            app.MapGet("/hello", handler);

            // Here’s an example of a local function:
            string Hello() => "[LOCAL FUNCTION] Hello World!";
            app.MapGet("/hello-local-function", Hello);

            // The following is an example of an instance method:
            var handlerInstance = new HelloHandler();
            app.MapGet("/hello-instance-object-method", handlerInstance.Hello);

            // Here, we can see an example of a static method:

            app.MapGet("/hello-static-method", HelloHandler.HelloStatic);

            // Route parameters

            app.MapGet("/users/{username}/products/{productId}",
                (string username, int productId) => $"The Username is {username} and the product Id is { productId }");

            // route Constrains 
            app.MapGet("/users/{id:int}", (int id) => $"The user Id is {id}");
            app.MapGet("/users2/{id:guid}", (Guid id) => $"The user Guid is {id}");
            // Parameter Binding 
            app.MapGet("/search", (string q) => { });
            app.MapGet("/search2", ([FromQuery(Name = "q")] string searchText) => { });
            // Spacial parameter Binding
            app.MapGet("/products", (HttpContext context, HttpRequest req, HttpResponse res, ClaimsPrincipal user) => {

                var con = context;
                var r = req;
                var rs = res;

            });


            // GET /navigate?location=43.8427,7.8527
            app.MapGet("/navigate", (Location location) => $"Location: { location.Latitude}, { location.Longitude}   ");

            // Exploring responses
            // we can directly return  a string or a class (either synchronously or asynchronously) :

                        app.MapGet("/ok", () => Results.Ok(new Person("Donald", "Duck")));
            app.MapGet("/notfound", () => Results.NotFound());
            app.MapPost("/badrequest", () =>
            {
                // Creates a 400 response with a JSON body.
                return Results.BadRequest(new { ErrorMessage = "Unable to complete the request" });
            });

            app.MapGet("/download", (string fileName) => Results.File(fileName));



            // MapEndpoints
            PeopleHandler.MapEndpoints(app);

            app.Run();
        }
    }
}