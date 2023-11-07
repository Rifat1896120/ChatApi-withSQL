using Microsoft.EntityFrameworkCore;
using ChatApi.Data;
using ChatApi.Model;
namespace ChatApi;

public static class FriendModelEndpoints
{
    public static void MapFriendModelEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/FriendModel", async (ChatApiContext db) =>
        {
            return await db.FriendModel.ToListAsync();
        })
        .WithName("GetAllFriendModels")
        .Produces<List<FriendModel>>(StatusCodes.Status200OK);

        routes.MapGet("/api/FriendModel/{id}", async (int Id, ChatApiContext db) =>
        {
            return await db.FriendModel.FindAsync(Id)
                is FriendModel model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetFriendModelById")
        .Produces<FriendModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/FriendModel/{id}", async (int Id, FriendModel friendModel, ChatApiContext db) =>
        {
            var foundModel = await db.FriendModel.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(friendModel);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateFriendModel")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/FriendModel/", async (FriendModel friendModel, ChatApiContext db) =>
        {
            db.FriendModel.Add(friendModel);
            await db.SaveChangesAsync();
            return Results.Created($"/FriendModels/{friendModel.Id}", friendModel);
        })
        .WithName("CreateFriendModel")
        .Produces<FriendModel>(StatusCodes.Status201Created);

        routes.MapDelete("/api/FriendModel/{id}", async (int Id, ChatApiContext db) =>
        {
            if (await db.FriendModel.FindAsync(Id) is FriendModel friendModel)
            {
                db.FriendModel.Remove(friendModel);
                await db.SaveChangesAsync();
                return Results.Ok(friendModel);
            }

            return Results.NotFound();
        })
        .WithName("DeleteFriendModel")
        .Produces<FriendModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
