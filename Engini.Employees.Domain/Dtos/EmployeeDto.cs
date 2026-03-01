namespace Engini.Employees.Domain.Dtos;

public class EmployeeDto
{
    public int Id { get; set; }
    public int? ManagerId { get; set; }
    public string Name { get; set; }
    
    public EmployeeDto? Manager { get; set; }
    public List<EmployeeDto>? Subordinates { get; set; }
}