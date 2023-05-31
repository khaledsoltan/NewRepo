using Abdulrhmaan.News.APIs;
using Abdulrhmaan.News.APIs.UserServices;
using Abdulrhmaan.News.SQlServer;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container(configuring Services).
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NewsContext>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.RegisterMapsterConfiguration();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<NewsContext>();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// minimanl apis
APIRouteHandler.RegisterAuthenticationAPIs(app);


app.Run();

