using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TodoDBContext>(
    opt => opt.UseMySQL("Server=localhost;Port=3306;Database=todo_csharp;User=dev;Password=groovie"));
builder.Services.AddDbContext<UserDBContext>(
    opt => opt.UseMySQL("Server=localhost;Port=3306;Database=todo_csharp;User=dev;Password=groovie"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
