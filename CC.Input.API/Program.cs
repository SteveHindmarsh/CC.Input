using CC.Input.Data;
using CC.Input.Logic;
using CC.Input.Logic.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Database
var connectionStringName = "DefaultConnection";
var connectionString = builder.Configuration.GetConnectionString(connectionStringName) ?? throw new InvalidOperationException($"Connection string '{connectionStringName}' not found.");
builder.Services.AddDbContext<InputDbContext>(options => options.UseSqlServer(connectionString));//, b => b.MigrationsAssembly("Everflow.Data.Sql.Event")); 
builder.Services.AddScoped<IValidationController, ValidationController>();
builder.Services.AddScoped<IRepository<Input>, CC.Input.Logic.Database.Repository>();

//DB Mocks (instead of everything under 'Database' above)
//builder.Services.AddSingleton<IValidationController, ValidationController>();
//builder.Services.AddSingleton<IRepository<Input>, CC.Input.Logic.Mock.Repository>();

builder.Services.AddControllers();
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
