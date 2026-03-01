namespace Engini.Employees.Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public int? ManagerId { get; set; }
    public string Name { get; set; }
    
    public Employee Manager { get; set; }
    public List<Employee> Subordinates { get; set; }
}