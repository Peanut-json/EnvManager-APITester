using EnvironmentManager.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//--------

// building the SQL server instence form the connection string provided
builder.Services.AddDbContext<EnvironmentDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("EnvironmentManagerConnectionString")));


//--------



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()); // enable CORS for testing 

app.UseAuthorization();

app.MapControllers();

app.Run();
