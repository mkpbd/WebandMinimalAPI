using Microsoft.AspNetCore.Mvc;

namespace Chapter02.Handler
{
    public class PeopleHandler
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/people", GetList);
            app.MapGet("/api/people/{id:guid}", Get);
            app.MapPost("/api/people", Insert);
            app.MapPut("/api/people/{id:guid}", Update);
            app.MapDelete("/api/people/{id:guid}", Delete);
        }
        private static IResult GetList([FromBody]PeopleService peopleService)
        {
            /* ... */

            return Results.Ok();
        }
        private static IResult Get(Guid id,[FromBody] PeopleService peopleService)
        {
            /* ... */
            return Results.Ok();
        }
        private static IResult Insert( [FromBody] PeopleService people)
        {
            /* ... */
            return Results.Ok();
        }
        private static IResult Update(Guid id,  [FromBody] PeopleService people)
        {
            /* ... */
            return Results.Ok();

        }
        private static IResult Delete(Guid id)
        {
            return Results.Ok();
            /* ... */
        }

    }
}
