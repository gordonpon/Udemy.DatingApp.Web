using Microsoft.EntityFrameworkCore;
using Udemy.DatingApp.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
   opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); 
});

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(f => f.AllowAnyHeader()
                  .AllowAnyMethod()
                  .WithOrigins("https://localhost:4200", "http://localhost:4200"));

app.MapControllers();

app.Run();
