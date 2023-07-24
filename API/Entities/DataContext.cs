using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using API.Entities.SalaryManagement;
using API.Entities.LeaveManagement;



namespace API.Entities
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options) { }



        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<SalaryPayment> SalaryPayments { get; set; }
        public DbSet<Leave> Leaves { get; set; }

        public DbSet<LeaveType> LeaveTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new LeaveMap());
            builder.ApplyConfiguration(new SalaryPaymentMap());
            builder.ApplyConfiguration(new EmployeeMap());
            builder.ApplyConfiguration(new DepartmentMap());
            builder.ApplyConfiguration(new DesignationMap());
        }

    }
}