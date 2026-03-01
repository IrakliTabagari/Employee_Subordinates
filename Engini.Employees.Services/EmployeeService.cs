using Engini.Employees.Domain.Dtos;
using Engini.Employees.Domain.Interfaces.Services;
using Engini.Employees.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Engini.Employees.Services;

public class EmployeeService : IEmployeeService
{
    private readonly EmployeesDbContext _dbContext;

    public EmployeeService(EmployeesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EmployeeDto?> GetEmployeeAsync(int id)
    {
        var allEmployees = await _dbContext.Employees
            .FromSqlInterpolated($@"
                    WITH EmpCTE AS (
                        SELECT Id, ManagerId, Name
                        FROM Employees
                        WHERE Id = {id}

                        UNION ALL

                        SELECT e.Id, e.ManagerId, e.Name
                        FROM Employees e
                        INNER JOIN EmpCTE p ON e.ManagerId = p.Id
                    )
                    SELECT Id, ManagerId, Name
                    FROM EmpCTE
                    OPTION (MAXRECURSION 100);
                    ")
            .AsNoTracking()
            .Select(e => new EmployeeDto()
            {
                Id = e.Id,
                ManagerId = e.ManagerId,
                Name = e.Name,
                Manager = null,
                Subordinates = new List<EmployeeDto>(),
            })
            .OrderBy(e => e.Id)
            .ToListAsync();

        if (allEmployees.Count == 0)
            return null;
        
        var employeesById = new Dictionary<int, EmployeeDto>();
        
        foreach (var emp in allEmployees)
        {
            employeesById[emp.Id] = emp;
        }
        
        foreach (var emp in employeesById.Values)
        {
            if (!emp.ManagerId.HasValue) 
                continue;
            var managerId = emp.ManagerId.Value;

            if (!employeesById.TryGetValue(managerId, out var manager)) 
                continue;
            
            emp.Manager = manager;
            manager.Subordinates.Add(emp);
        }

        return employeesById[id];
    }
}