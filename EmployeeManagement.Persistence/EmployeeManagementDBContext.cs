using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Application.Contracts;
using EmployeeManagment.Domain.Common;
using EmployeeManagment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Persistence
{
    public class EmployeeManagementDBContext : DbContext
    {
        private readonly ILoggedInUserService loggedInUserService;

        public EmployeeManagementDBContext(DbContextOptions<EmployeeManagementDBContext> options) : base(options)
        {

        }

        public EmployeeManagementDBContext(DbContextOptions<EmployeeManagementDBContext> options, ILoggedInUserService loggedInUserService)
            : base(options)
        {
            this.loggedInUserService = loggedInUserService;
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeManagementDBContext).Assembly);

            //seed data, added through migrations
            var sdId = 1;
            var hrId = 2;
            var erpId = 3;

            modelBuilder.Entity<Department>().HasData(new Department
            {
                Id = sdId,
                Name = "Software Development",
                Alias = "SD",
                Description = "Responsible of developing new solutions and maintain existing ones"
            });
            modelBuilder.Entity<Department>().HasData(new Department
            {
                Id = hrId,
                Name = "Human Resources",
                Alias = "HR",
                Description = "Manage Human Resources"
            });
            modelBuilder.Entity<Department>().HasData(new Department
            {
                Id = erpId,
                Name = "Enterprise Resource Planning",
                Alias = "ERP",
                Description = "Responsible of Dealing with the clients"
            });
            

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Name = "John Egbert",
                Id = 1,
                DepartmentId = sdId,
                Email = "JEgbert@EmployeeManagment.com",
                PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/banjo.jpg",
            });


            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                Name = "Michael Johnson",
                Id = 2,
                DepartmentId = sdId,
                Email = "MJohnson@EmployeeManagement.com",
                PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/michael.jpg",
            });

            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                Name = "Sam Jerktron",
                Id = 3,
                DepartmentId = hrId,
                Email = "SJerktron@EmployeeManagement.com",
                PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/dj.jpg",
            });

            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                Name = "Manuel Santinonisi",
                Id = 4,
                DepartmentId = hrId,
                Email = "MSantinonisi@EmployeeManagement.com",
                PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/guitar.jpg",
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Name = "ManyMony",
                Id = 5,
                DepartmentId = erpId,
                Email = "MMony@EmployeeManagement.com",
                PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/conf.jpg",
            });

            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                Name = "Nick Sailor",
                Id = 6,
                DepartmentId = erpId,
                Email = "NSailor@EmployeeManagement.com",
                PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/musical.jpg",
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = loggedInUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = loggedInUserService.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
