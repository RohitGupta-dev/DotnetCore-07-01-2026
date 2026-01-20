using LearningDotnet.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Add Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Minute)
    .CreateLogger();

builder.Host.UseSerilog();
    
    // Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

////add Method for content negotiation
//builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


    
// add Dependency Injection 
builder.Services.AddSingleton<IMyLogger, LogToFile>();
//builder.Services.AddScoped<IMyLogger, LogToDB>();
//builder.Services.AddTransient<IMyLogger, LogToServerMemory>();


builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

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
