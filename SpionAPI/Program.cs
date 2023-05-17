using Microsoft.EntityFrameworkCore;
using Serilog;
using SpionAPI.Interfaces;
using SpionAPI.Repositories;
using SpionAPI_DataAccess.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//connection to DB stuff:
String connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information);
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    //policies - Allow all requests
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());

});


//Add logging here
builder.Host.UseSerilog((context,logConf) => logConf.WriteTo.Console().ReadFrom.Configuration(context.Configuration) );

//add/register my own sevices here:
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IGuessDataRepository, GuessDataRepository>();
builder.Services.AddScoped<IGameStatsRepository, GameStatsRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Add auto logging for client requests
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
