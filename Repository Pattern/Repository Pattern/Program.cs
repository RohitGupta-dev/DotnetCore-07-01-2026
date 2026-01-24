using Microsoft.EntityFrameworkCore;
using Repository_Pattern.Data;
using Repository_Pattern.Models;
using Repository_Pattern.Repo;
using Repository_Pattern.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddScoped<IEmployee,EmployeeRepositry>();
builder.Services.AddScoped<IStudent,StudentRepositry>();

builder.Services.AddScoped(typeof(ICollegeRepository<>),typeof(collegeRepository<>));
//builder.Services.AddScoped(typeof(ICollegeRepository<>),typeof(collegeRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
