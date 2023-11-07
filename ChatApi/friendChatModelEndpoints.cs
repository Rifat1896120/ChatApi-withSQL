using Microsoft.EntityFrameworkCore;
using ChatApi.Data;
using ChatApi.Model;
namespace ChatApi;

public static class friendChatModelEndpoints
{
    public static void MapfriendChatModelEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/friendChatModel", async (ChatApiContext db) =>
        {
            return await db.friendChatModel.ToListAsync();
        })
        .WithName("GetAllfriendChatModels")
        .Produces<List<friendChatModel>>(StatusCodes.Status200OK);

        routes.MapGet("/api/friendChatModel/{id}", async (int Id, ChatApiContext db) =>
        {
            return await db.friendChatModel.FindAsync(Id)
                is friendChatModel model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetfriendChatModelById")
        .Produces<friendChatModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/friendChatModel/{id}", async (int Id, friendChatModel friendChatModel, ChatApiContext db) =>
        {
            var foundModel = await db.friendChatModel.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(friendChatModel);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdatefriendChatModel")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/friendChatModel/", async (friendChatModel friendChatModel, ChatApiContext db) =>
        {
            db.friendChatModel.Add(friendChatModel);
            await db.SaveChangesAsync();
            return Results.Created($"/friendChatModels/{friendChatModel.Id}", friendChatModel);
        })
        .WithName("CreatefriendChatModel")
        .Produces<friendChatModel>(StatusCodes.Status201Created);

        routes.MapDelete("/api/friendChatModel/{id}", async (int Id, ChatApiContext db) =>
        {
            if (await db.friendChatModel.FindAsync(Id) is friendChatModel friendChatModel)
            {
                db.friendChatModel.Remove(friendChatModel);
                await db.SaveChangesAsync();
                return Results.Ok(friendChatModel);
            }

            return Results.NotFound();
        })
        .WithName("DeletefriendChatModel")
        .Produces<friendChatModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
