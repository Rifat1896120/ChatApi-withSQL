using Microsoft.EntityFrameworkCore;
using ChatApi.Data;
using ChatApi.Model;
namespace ChatApi;

public static class RequestsEndpoints
{
    public static void MapRequestsEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Requests", async (ChatApiContext db) =>
        {
            return await db.Requests.ToListAsync();
        })
        .WithName("GetAllRequestss")
        .Produces<List<Requests>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Requests/{id}", async (int Id, ChatApiContext db) =>
        {
            return await db.Requests.FindAsync(Id)
                is Requests model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetRequestsById")
        .Produces<Requests>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Requests/{id}", async (int Id, Requests requests, ChatApiContext db) =>
        {
            var foundModel = await db.Requests.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(requests);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateRequests")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Requests/", async (Requests requests, ChatApiContext db) =>
        {
            db.Requests.Add(requests);
            await db.SaveChangesAsync();
            return Results.Created($"/Requestss/{requests.Id}", requests);
        })
        .WithName("CreateRequests")
        .Produces<Requests>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Requests/{id}", async (int Id, ChatApiContext db) =>
        {
            if (await db.Requests.FindAsync(Id) is Requests requests)
            {
                db.Requests.Remove(requests);
                await db.SaveChangesAsync();
                return Results.Ok(requests);
            }

            return Results.NotFound();
        })
        .WithName("DeleteRequests")
        .Produces<Requests>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
