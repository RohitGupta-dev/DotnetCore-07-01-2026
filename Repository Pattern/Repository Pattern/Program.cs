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

//builder.Services.AddCors(option=>option.AddPolicy("test",
//    //policy=>policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod())
//    policy=>policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod())

//    );

builder.Services.AddCors(options=>{
    options.AddDefaultPolicy(policy=>policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    options.AddPolicy("AllowAll",policy=>policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    options.AddPolicy("onlyLoclhost",policy=>policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
    options.AddPolicy("test",policy=>policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors("test");
app.UseRouting();
app.UseCors();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("api/testingEnspoints", context => context.Response.WriteAsync("Test Response")).RequireCors();
    endpoints.MapControllers().RequireCors("AllowAll");
    endpoints.MapGet("api/testingEnspoint2", context => context.Response.WriteAsync("Test Response 2")).RequireCors();

});

app.MapControllers();

app.Run();
