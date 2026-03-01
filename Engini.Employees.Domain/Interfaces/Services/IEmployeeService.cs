using Engini.Employees.Domain.Dtos;

namespace Engini.Employees.Domain.Interfaces.Services;

public interface IEmployeeService
{
    Task<EmployeeDto?> GetEmployeeAsync(int id);
}