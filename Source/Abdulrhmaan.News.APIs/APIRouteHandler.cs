using Abdulrhmaan.News.APIs.UserServices;
using Abdulrhmaan.News.SQlServer;
using Abdulrhmaan.NewsSite.Data;
using Abdulrhmaan.NewsSite.Data.Exceptions;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using Z.EntityFramework.Plus;

namespace Abdulrhmaan.News.APIs;

public static class APIRouteHandler
{

    public static void RegisterAuthenticationAPIs(WebApplication app)
    {
        app.MapGet("/GetAllNews", GetNews)
.WithName("GetAllNews")
.WithOpenApi();

        app.MapGet("/GetItemById", GetItemById)
.WithName("GetItemById")
.WithOpenApi();

        app.MapPost("/Register", CreateUser)
.WithName("Register")
.WithOpenApi();

        app.MapPost("/AddNews", AddNews)
.WithName("AddNews")
.WithOpenApi();

        app.MapPost("/DeleteNews", DeleteNews)
.WithName("DeleteNews")
.WithOpenApi();

        app.MapPost("/EditNews", EditNews)
.WithName("EditNews")
.WithOpenApi();

    }



    static async Task<IResult> CreateUser([FromBody] RegisterUser RequestUser, [FromServices] IUserService UserService)
    {
        var result = await UserService.RegisterUser(RequestUser);
        var Errors = new List<Error>();
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                var Error = new Error { Code = error.Code, Description = error.Description };
                Errors.Add(Error);
            }
            return TypedResults.BadRequest(Errors);
        }
        return TypedResults.Ok(result.Succeeded);
    }

    //Iam applying soft delete here 
    static async Task<IResult> DeleteNews([FromServices] NewsContext context , int NewsId)
    {
        var NewsToDelete = await context.Items.FindAsync(NewsId);

        if (NewsToDelete is not null)
        {
            await context.Items.UpdateAsync(e => e.IsDeleted == true);
            return TypedResults.Ok(NewsToDelete);
        }
        return TypedResults.NoContent();
    }

    static async Task<IResult> EditNews([FromServices] NewsContext context, NewsDto NewsDto)
    {
        var NewsToUpdate = NewsDto.Adapt<Item>();
        context.Items.Update(NewsToUpdate);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    static async Task<IResult> AddNews([FromBody] NewsDto news, [FromServices] NewsContext context)
    {
        var NewsToAdd = news.Adapt<Item>();
        NewsToAdd.InsertedAt = DateTime.Now;
        await context.Items.AddAsync(NewsToAdd);
        await context.SaveChangesAsync();
        return TypedResults.Ok(NewsToAdd);
    }

    static async Task<List<NewsDto>> GetNews([FromServices] NewsContext Context)
    {
        var Items = await Context.Items.ToListAsync();
        return Items.Adapt<List<NewsDto>>();
    }

    static async Task<NewsDto> GetItemById([FromServices] NewsContext Context, int ItemId)
    {
        var Items = await Context.Items.FindAsync(ItemId);
        return Items.Adapt<NewsDto>();
    }
}

