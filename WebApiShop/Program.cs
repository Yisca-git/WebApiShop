
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserPasswordRepository, UserPasswordRepository>();
builder.Services.AddScoped<IUserPasswordService, UserPasswordService>();
builder.Services.AddDbContext<Shop_WebApiContext>(options => options.UseSqlServer
("Data Source = DESKTOP - 55334A9; Initial Catalog = Shop_WebApi; Integrated Security = True; Trust Server Certificate=True"));


builder.Services.AddControllers();

var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();

