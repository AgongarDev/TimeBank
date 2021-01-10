using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeBank.Core.DataAccess
{
    public class TimeBankContextFactory : IDesignTimeDbContextFactory<TimeBankContext>
    {
        public TimeBankContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TimeBankContext>();
            ConnectionData con = new ConnectionData();
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-BI3GKO5\SQLEXPRESS;Initial Catalog="TimeBankDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new TimeBankContext(optionsBuilder.Options);
        }
    }
}
