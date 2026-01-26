using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository_Pattern.Data;
using Repository_Pattern.Models;
using Repository_Pattern.Repo;
using Repository_Pattern.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecret"));
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

//jwt

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(option =>
{
    //option.RequireHttpsMetadata = false;
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,

    };
    
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
    endpoints.MapGet("api/testingEnspoint2", context => context.Response.WriteAsync(builder.Configuration.GetValue<string>("JWTSecret"))).RequireCors();

});

app.MapControllers();

app.Run();
