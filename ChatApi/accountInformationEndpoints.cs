using Microsoft.EntityFrameworkCore;
using ChatApi.Data;
using ChatApi.Model;
namespace ChatApi;

public static class accountInformationEndpoints
{
    public static void MapaccountInformationEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/accountInformation", async (ChatApiContext db) =>
        {
            return await db.accountInformation.ToListAsync();
        })
        .WithName("GetAllaccountInformations")
        .Produces<List<accountInformation>>(StatusCodes.Status200OK);

        routes.MapGet("/api/accountInformation/{id}", async (int Id, ChatApiContext db) =>
        {
            return await db.accountInformation.FindAsync(Id)
                is accountInformation model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetaccountInformationById")
        .Produces<accountInformation>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/accountInformation/{id}", async (int Id, accountInformation accountInformation, ChatApiContext db) =>
        {
            var foundModel = await db.accountInformation.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(accountInformation);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateaccountInformation")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/accountInformation/", async (accountInformation accountInformation, ChatApiContext db) =>
        {
            db.accountInformation.Add(accountInformation);
            await db.SaveChangesAsync();
            return Results.Created($"/accountInformations/{accountInformation.Id}", accountInformation);
        })
        .WithName("CreateaccountInformation")
        .Produces<accountInformation>(StatusCodes.Status201Created);

        routes.MapDelete("/api/accountInformation/{id}", async (int Id, ChatApiContext db) =>
        {
            if (await db.accountInformation.FindAsync(Id) is accountInformation accountInformation)
            {
                db.accountInformation.Remove(accountInformation);
                await db.SaveChangesAsync();
                return Results.Ok(accountInformation);
            }

            return Results.NotFound();
        })
        .WithName("DeleteaccountInformation")
        .Produces<accountInformation>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
