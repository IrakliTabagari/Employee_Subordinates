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
    }
}