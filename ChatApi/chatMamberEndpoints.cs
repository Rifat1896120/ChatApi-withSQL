using Microsoft.EntityFrameworkCore;
using ChatApi.Data;
using ChatApi.Model;
namespace ChatApi;

public static class chatMamberEndpoints
{
    public static void MapchatMamberEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/chatMamber", async (ChatApiContext db) =>
        {
            return await db.chatMamber.ToListAsync();
        })
        .WithName("GetAllchatMambers")
        .Produces<List<chatMamber>>(StatusCodes.Status200OK);

        routes.MapGet("/api/chatMamber/{id}", async (int Id, ChatApiContext db) =>
        {
            return await db.chatMamber.FindAsync(Id)
                is chatMamber model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetchatMamberById")
        .Produces<chatMamber>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/chatMamber/{id}", async (int Id, chatMamber chatMamber, ChatApiContext db) =>
        {
            var foundModel = await db.chatMamber.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(chatMamber);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdatechatMamber")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/chatMamber/", async (chatMamber chatMamber, ChatApiContext db) =>
        {
            db.chatMamber.Add(chatMamber);
            await db.SaveChangesAsync();
            return Results.Created($"/chatMambers/{chatMamber.Id}", chatMamber);
        })
        .WithName("CreatechatMamber")
        .Produces<chatMamber>(StatusCodes.Status201Created);

        routes.MapDelete("/api/chatMamber/{id}", async (int Id, ChatApiContext db) =>
        {
            if (await db.chatMamber.FindAsync(Id) is chatMamber chatMamber)
            {
                db.chatMamber.Remove(chatMamber);
                await db.SaveChangesAsync();
                return Results.Ok(chatMamber);
            }

            return Results.NotFound();
        })
        .WithName("DeletechatMamber")
        .Produces<chatMamber>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
