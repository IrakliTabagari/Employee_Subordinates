using Engini.Employees.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engini.Employees.Infrastructure;

public class EmployeesDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Employee>(ConfigureEmployees);
        
    }
    
    public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options)
        : base(options)
    {
    }
    
    private static void ConfigureEmployees(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees", "dbo");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name)
            .HasMaxLength(250)
            .IsRequired();
        builder
            .HasOne(e => e.Manager)
            .WithMany(x => x.Subordinates)
            .HasForeignKey(e => e.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.ManagerId);
        
        builder.HasData(
            new Employee { Id = 1, ManagerId = null, Name = "Alice (CEO)" },

            new Employee { Id = 2, ManagerId = 1, Name = "Bob (Head of Engineering)" },
            new Employee { Id = 3, ManagerId = 2, Name = "Carol (Engineering Manager)" },
            new Employee { Id = 4, ManagerId = 3, Name = "Dan (Team Lead)" },
            new Employee { Id = 5, ManagerId = 4, Name = "Eva (Senior Developer)" },
            new Employee { Id = 6, ManagerId = 4, Name = "Frank (Developer)" },
            new Employee { Id = 7, ManagerId = 3, Name = "Grace (QA Engineer)" },

            new Employee { Id = 8, ManagerId = 1, Name = "Henry (Head of HR)" },
            new Employee { Id = 9, ManagerId = 8, Name = "Irene (Recruiter)" }
        );
    }
}