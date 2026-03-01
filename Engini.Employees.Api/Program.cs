using Engini.Employees.Domain.Interfaces.Services;
using Engini.Employees.Infrastructure;
using Engini.Employees.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<EmployeesDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Engini.Employees.Api")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
