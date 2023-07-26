using Microsoft.EntityFrameworkCore;
using System;

using System.Collections.Generic;

namespace AspT3
{
    public class EmployeeDbContext :DbContext
    {
        private string connectionString = @"Data Source=CNTT-TRUNGNV-PC,1433;Initial Catalog=MANAGER;User Id=sa;Password=@Automation1;TrustServerCertificate=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Employee> Employee { get; set; }
    }
}
