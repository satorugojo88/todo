using System.Text.Json;
using api;
using efscaffold;
using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var appOptions = builder.Services.AddAppOptions(builder.Configuration);

Console.WriteLine("The appOptions: "+JsonSerializer.Serialize(appOptions));

builder.Services.AddDbContext<TodoDbContext>(options =>
{
  options.UseNpgsql(appOptions.DbConnectionString);
});

var app = builder.Build();

app.MapGet("/", (
  [FromServices]IOptionsMonitor<AppOptions> optionsMonitor,
  [FromServices]TodoDbContext db
  ) =>
{
  var myTodo = new Todo
  {
    Id = Guid.NewGuid().ToString(),
    Description = "Getting things done mindset",
    Title = "Organize my daily routine",
    Completed = false,
    Priority = 5,
    CreatedAt = DateTime.Now
  };
  db.Todos.Add(myTodo);
  db.SaveChanges();
  
  var results = db.Todos.ToList();
  
  return results;
});

app.Run();
